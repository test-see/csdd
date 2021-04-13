using foundation.ef5;
using foundation.mediator;
using irespository.client.goods.model;
using Mediator.Net;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using Microsoft.EntityFrameworkCore;
using nouns.client.profile;
using storage.client.goods2hospitalgoods.carrier;
using storage.clientgoods2hospitalgoods.carrier;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client.profile
{
    public class GetClientGoodsStorageRequestHandler : IRequestHandler<StorageRequest<GetClientGoodsRequest>, GetResponse<GetClientGoodsResponse>>
    {
        private readonly DefaultDbContext _context;
        private readonly IMediator _mediator;
        public GetClientGoodsStorageRequestHandler(DefaultDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<GetResponse<GetClientGoodsResponse>> Handle(IReceiveContext<StorageRequest<GetClientGoodsRequest>> context, CancellationToken cancellationToken)
        {
            var payload = context.Message.Payload;
            var sql = from r in _context.ClientGoods
                      join u in _context.User on r.CreateUserId equals u.Id
                      join h in _context.Client on r.ClientId equals h.Id
                      where payload.Ids.Contains(r.Id)
                      select new GetClientGoodsResponse
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Name = r.Name,
                          Code = r.Code,
                          Client = new GetClientResponse
                          {
                              Id = h.Id,
                              Name = h.Name,
                          },
                          Producer = r.Producer,
                          Spec = r.Spec,
                          Unit = r.Unit,
                          CreateUserName = u.Username,
                          IsActive = r.IsActive,

                      };

            var profiles = await sql.ToListAsync();

            var request = new StorageRequest<ListClientGoods2HospitalGoodsRequest>(
                new ListClientGoods2HospitalGoodsRequest { ClientGoodsIds = profiles.Select(x => x.Id).ToArray(), });
            var mappings = await _mediator.RequestAsync<StorageRequest<ListClientGoods2HospitalGoodsRequest>, GetResponse<ListClientGoods2HospitalGoodsResponse>>(request);

            foreach (var profile in profiles)
            {
                profile.Mappings = mappings.Payloads.Where(x => x.ClientGoodsId == profile.Id).ToList();
            }
            return new GetResponse<GetClientGoodsResponse>(profiles.ToArray());
        }
    }
}
