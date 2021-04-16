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
    public class GetHospitalRequestHandler : IRequestHandler<GetHospitalRequest, ListResponse<GetHospitalResponse>>
    {
        private readonly DefaultDbContext _context;
        public GetHospitalRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<ListResponse<GetHospitalResponse>> Handle(IReceiveContext<GetHospitalRequest> context, CancellationToken cancellationToken)
        {
            var payload = context.Message;
            var hospitals = await _context.Hospital.Where(x => payload.Ids.Contains(x.Id)).Select(x => new GetHospitalResponse
            {
                ConsumeDays = x.ConsumeDays,
                Id = x.Id,
                Name = x.Name,
                Remark = x.Remark,
            }).ToListAsync();

            return hospitals.ToResponse();
        }
    }
}
