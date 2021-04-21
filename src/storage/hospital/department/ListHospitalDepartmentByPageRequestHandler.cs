using foundation.config;
using foundation.ef5;
using foundation.mediator;
using irespository.hospital.department.model;
using irespository.hospital.profile.model;
using Mediator.Net;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using nouns.client.profile;
using storage.hospitaldepartment.carrier;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.request.client
{
    public class ListHospitalDepartmentByPageRequestHandler : IRequestHandler<PagerQuery<ListHospitalDepartmentRequest>, PagerResult<ListHospitalDepartmentResponse>>
    {
        private readonly DefaultDbContext _context;
        private readonly IMediator _mediator;
        public ListHospitalDepartmentByPageRequestHandler(DefaultDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<PagerResult<ListHospitalDepartmentResponse>> Handle(IReceiveContext<PagerQuery<ListHospitalDepartmentRequest>> context, CancellationToken cancellationToken)
        {
            var query = context.Message;
            var sql = from r in _context.HospitalDepartment
                      join u in _context.User on r.CreateUserId equals u.Id
                      join d in _context.DataDepartmentType on r.DepartmentTypeId equals d.Id
                      join rp in _context.HospitalDepartment on r.ParentId equals rp.Id into rp_def
                      from rp_def_t in rp_def.DefaultIfEmpty()
                      orderby r.Id descending
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
            if (query.Query?.HospitalId != null)
            {
                sql = sql.Where(x => x.Hospital.Id == query.Query.HospitalId.Value);
            }
            if (query.Query?.DepartmentTypeId != null)
            {
                sql = sql.Where(x => x.DepartmentType.Id == query.Query.DepartmentTypeId.Value);
            }
            if (!string.IsNullOrEmpty(query.Query?.Name))
            {
                sql = sql.Where(x => x.Name.Contains(query.Query.Name));
            }
            var data = new PagerResult<ListHospitalDepartmentResponse>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                var hospitals = await _mediator.ListByIdsAsync<GetHospitalRequest, GetHospitalResponse>(data.Select(x => x.Hospital.Id).ToList());

                foreach (var m in data.Result)
                {
                    m.Hospital = hospitals.FirstOrDefault(x => x.Id == m.Hospital.Id);
                }
            }
            return data;
        }
    }
}
