using foundation.ef5;
using foundation.mediator;
using irespository.hospital.client.model;
using irespository.hospital.profile.model;
using Mediator.Net;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using Microsoft.EntityFrameworkCore;
using nouns.client.profile;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client.profile
{
    public class GetHospitalClientRequestHandler : IRequestHandler<GetHospitalClientRequest, ListResponse<GetHospitalClientResponse>>
    {
        private readonly DefaultDbContext _context;
        private readonly IMediator _mediator;
        public GetHospitalClientRequestHandler(DefaultDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<ListResponse<GetHospitalClientResponse>> Handle(IReceiveContext<GetHospitalClientRequest> context, CancellationToken cancellationToken)
        {
            var payload = context.Message;
            var hospitalClients = await (from r in _context.HospitalClient
                                   join u in _context.User on r.CreateUserId equals u.Id
                                   join m in _context.Client2HospitalClient on r.Id equals m.HospitalClientId into md
                                   from mdd in md.DefaultIfEmpty()
                                   where payload.Ids.Contains(r.Id)
                                   select new GetHospitalClientResponse
                                   {
                                       Id = r.Id,
                                       Name = r.Name,
                                       Hospital = new GetHospitalResponse
                                       {
                                           Id = r.HospitalId,
                                       },
                                       Client = mdd != null ? new GetClientResponse { Id = mdd.ClientId } : null,
                                   }).ToListAsync();

            var dd = hospitalClients.Where(x => x.Client != null).Select(x => x.Client.Id).ToList();
            var sql = (from c in _context.Client
                       where dd.Contains(c.Id)
                       select new GetClientResponse
                       {
                           Id = c.Id,
                           Name = c.Name,
                       }).ToList();
            var hospitals = await _mediator.RequestListByIdsAsync<GetHospitalRequest, GetHospitalResponse>(hospitalClients.Select(x => x.Hospital.Id).ToArray());

            foreach (var c in hospitalClients)
            {
                if (c.Client != null) c.Client = sql.FirstOrDefault(x => x.Id == c.Client.Id);
                c.Hospital = hospitals.FirstOrDefault(x => x.Id == c.Hospital.Id);
            }

            return hospitalClients.ToResponse();
        }
    }
}
