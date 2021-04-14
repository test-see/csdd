using foundation.ef5;
using foundation.mediator;
using irespository.client.maping.model;
using irespository.hospital.client.model;
using Mediator.Net;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using Microsoft.EntityFrameworkCore;
using nouns.client.profile;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client.profile
{
    public class GetClientStorageRequestHandler : IRequestHandler<StorageRequest<GetClientRequest>, ListResponse<GetClientResponse>>
    {
        private readonly DefaultDbContext _context;
        private readonly IMediator _mediator;
        public GetClientStorageRequestHandler(DefaultDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<ListResponse<GetClientResponse>> Handle(IReceiveContext<StorageRequest<GetClientRequest>> context, CancellationToken cancellationToken)
        {
            var payload = context.Message.Payload;
            var clients = await (from r in _context.Client
                                 join u in _context.User on r.CreateUserId equals u.Id
                                 where payload.Ids.Contains(r.Id)
                                 select new GetClientResponse
                                 {
                                     CreateTime = r.CreateTime,
                                     Id = r.Id,
                                     Name = r.Name,
                                     CreateUserName = u.Username,
                                 }).ToListAsync();

            var sql = from p in _context.Client2HospitalClient
                      join c in _context.HospitalClient on p.HospitalClientId equals c.Id
                      join h in _context.Hospital on c.HospitalId equals h.Id
                      where payload.Ids.Contains(p.ClientId)
                      select new ListClient2HospitalClientResponse
                      {
                          Client = clients.First(x => x.Id == p.ClientId),
                          ClientMappingId = p.Id,
                          HospitalClient = new GetHospitalClientResponse
                          {
                              Id = c.Id,
                          },
                      };

            var mappings = await sql.ToListAsync();
            var request = new StorageRequest<GetHospitalClientRequest>(new GetHospitalClientRequest(mappings.Select(x => x.HospitalClient.Id).ToArray()));
            var hospitalclients = await _mediator.RequestAsync<StorageRequest<GetHospitalClientRequest>, ListResponse<GetHospitalClientResponse>>(request);
            foreach (var m in mappings)
            {
                m.HospitalClient = hospitalclients.Payloads.FirstOrDefault(x => x.Id == m.HospitalClient.Id);
            }
            foreach (var client in clients)
            {
                client.HospitalClients = mappings.Where(x => x.Client.Id == client.Id).ToList();
            }

            return new ListResponse<GetClientResponse>(clients.ToArray());
        }
    }
}
