using foundation.config;
using foundation.ef5;
using foundation.mediator;
using irespository.hospital.profile.model;
using Mediator.Net;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using Microsoft.EntityFrameworkCore;
using nouns.client.profile;
using storage.hospital.department.carrier;
using storage.hospitaldepartment.carrier;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.request.client
{
    public class ListHospitalDepartmentByHospitalIdRequestHandler : IRequestHandler<ListHospitalDepartmentByHospitalIdRequest, ListResponse<ListHospitalDepartmentResponse>>
    {
        private readonly DefaultDbContext _context;
        private readonly IMediator _mediator;
        public ListHospitalDepartmentByHospitalIdRequestHandler(DefaultDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ListResponse<ListHospitalDepartmentResponse>> Handle(IReceiveContext<ListHospitalDepartmentByHospitalIdRequest> context, CancellationToken cancellationToken)
        {
            var query = context.Message;
            var sql = from r in _context.HospitalDepartment
                      join u in _context.User on r.CreateUserId equals u.Id
                      join d in _context.DataDepartmentType on r.DepartmentTypeId equals d.Id
                      join rp in _context.HospitalDepartment on r.ParentId equals rp.Id into rp_def
                      from rp_def_t in rp_def.DefaultIfEmpty()
                      where r.HospitalId == query.HospitalId
                      select new ListHospitalDepartmentResponse
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Name = r.Name,
                          Hospital = new GetHospitalResponse
                          {
                              Id = r.HospitalId
                          },
                          CreateUserName = u.Username,
                          DepartmentType = d,
                          IsPurchaseCheck = r.IsPurchaseCheck,
                          Parent = rp_def_t != null ? new IdNameValueModel { Id = rp_def_t.Id, Name = rp_def_t.Name } : null,
                      };
            var data = await sql.ToListAsync();
            var hospitals = await _mediator.RequestListByIdsAsync<GetHospitalRequest, GetHospitalResponse>(data.Select(x => x.Hospital.Id).ToList());

            foreach (var m in data)
            {
                m.Hospital = hospitals.FirstOrDefault(x => x.Id == m.Hospital.Id);
            }
            return data.ToResponse();
        }
    }
}
