using foundation.ef5;
using foundation.mediator;
using irespository.hospital.profile.model;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using Microsoft.EntityFrameworkCore;
using nouns.client.profile;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client.profile
{
    public class GetHospitalStorageRequestHandler : IRequestHandler<StorageRequest<GetHospitalRequest>, GetResponse<GetHospitalResponse>>
    {
        private readonly DefaultDbContext _context;
        public GetHospitalStorageRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<GetResponse<GetHospitalResponse>> Handle(IReceiveContext<StorageRequest<GetHospitalRequest>> context, CancellationToken cancellationToken)
        {
            var payload = context.Message.Payload;
            var hospitals = await _context.Hospital.Where(x => payload.Ids.Contains(x.Id)).Select(x => new GetHospitalResponse
            {
                ConsumeDays = x.ConsumeDays,
                Id = x.Id,
                Name = x.Name,
                Remark = x.Remark,
            }).ToListAsync();

            return new GetResponse<GetHospitalResponse>(hospitals.ToArray());
        }
    }
}
