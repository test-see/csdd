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
    public class GetHospitalDepartmentStorageRequestHandler : IRequestHandler<StorageRequest<GetHospitalDepartmentRequest>, ListResponse<GetHospitalDepartmentResponse>>
    {
        private readonly DefaultDbContext _context;
        private readonly IMediator _mediator;
        public GetHospitalDepartmentStorageRequestHandler(DefaultDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<ListResponse<GetHospitalDepartmentResponse>> Handle(IReceiveContext<StorageRequest<GetHospitalDepartmentRequest>> context, CancellationToken cancellationToken)
        {
            var payload = context.Message.Payload;
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

            var request = new StorageRequest<GetHospitalRequest>(new GetHospitalRequest(profiles.Select(x => x.Hospital.Id).ToArray()));
            var hospitals = await _mediator.RequestAsync<StorageRequest<GetHospitalRequest>, ListResponse<GetHospitalResponse>>(request);

            foreach (var department in profiles)
            {
                department.Hospital = hospitals.Payloads.FirstOrDefault(x => x.Id == department.Hospital.Id);
            }

            return new ListResponse<GetHospitalDepartmentResponse>(profiles.ToArray());
        }
    }
}
