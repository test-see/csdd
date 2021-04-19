using foundation.ef5;
using foundation.mediator;
using irespository.hospital.department.model;
using irespository.hospital.profile.model;
using Mediator.Net;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using Microsoft.EntityFrameworkCore;
using nouns.client.profile;
using storage.hospital.department.carrier;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client.profile
{
    public class GetHospitalDepartmentRequestHandler : IRequestHandler<GetHospitalDepartmentRequest, ListResponse<GetHospitalDepartmentResponse>>
    {
        private readonly DefaultDbContext _context;
        private readonly IMediator _mediator;
        public GetHospitalDepartmentRequestHandler(DefaultDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<ListResponse<GetHospitalDepartmentResponse>> Handle(IReceiveContext<GetHospitalDepartmentRequest> context, CancellationToken cancellationToken)
        {
            var payload = context.Message;
            var sql = from r in _context.HospitalDepartment
                      join d in _context.DataDepartmentType on r.DepartmentTypeId equals d.Id
                      where payload.Ids.Contains(r.Id)
                      select new GetHospitalDepartmentResponse
                      {
                          Id = r.Id,
                          Name = r.Name,
                          Hospital = new GetHospitalResponse
                          {
                              Id = r.HospitalId,
                          },
                          DepartmentType = d,
                      };
            var profiles = await sql.ToListAsync();
            var hospitals = await _mediator.ListByIdsAsync<GetHospitalRequest, GetHospitalResponse>(profiles.Select(x => x.Hospital.Id).ToArray());

            foreach (var department in profiles)
            {
                department.Hospital = hospitals.FirstOrDefault(x => x.Id == department.Hospital.Id);
            }

            return profiles.ToResponse();
        }
    }
}
