using foundation.ef5;
using foundation.mediator;
using irespository.hospital.goods.model;
using Mediator.Net;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using Microsoft.EntityFrameworkCore;
using storage.client.goods2hospitalgoods.carrier;
using storage.clientgoods2hospitalgoods.carrier;
using storage.hospitalgoods.carrier;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class ListClientGoods2HospitalGoodsStorageRequestHandler : IRequestHandler<StorageRequest<ListClientGoods2HospitalGoodsRequest>, ListResponse<ListClientGoods2HospitalGoodsResponse>>
    {
        private readonly DefaultDbContext _context;
        private readonly IMediator _mediator;
        public ListClientGoods2HospitalGoodsStorageRequestHandler(DefaultDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ListResponse<ListClientGoods2HospitalGoodsResponse>> Handle(IReceiveContext<StorageRequest<ListClientGoods2HospitalGoodsRequest>> context, CancellationToken cancellationToken)
        {
            var payload = context.Message.Payload;
            var mappings = await (from m in _context.ClientGoods2HospitalGoods
                                  where payload.ClientGoodsIds.Contains(m.ClientGoodsId)
                                  select new ListClientGoods2HospitalGoodsResponse
                                  {
                                      Id = m.Id,
                                      ClientGoodsId = m.ClientGoodsId,
                                      ClientQty = m.ClientQty,
                                      HospitalQty = m.HospitalQty,
                                      HospitalGoods = new GetHospitalGoodsResponse
                                      {
                                          Id = m.HospitalGoodsId,
                                      }
                                  }).ToListAsync();

            var request = new StorageRequest<GetHospitalGoodsRequest>(new GetHospitalGoodsRequest(mappings.Select(x => x.HospitalGoods.Id).ToArray()));
            var goods = await _mediator.RequestAsync<StorageRequest<GetHospitalGoodsRequest>, ListResponse<GetHospitalGoodsResponse>>(request);

            foreach (var m in mappings)
            {
                m.HospitalGoods = goods.Payloads.FirstOrDefault(x => x.Id == m.HospitalGoods.Id);
            }
            return new ListResponse<ListClientGoods2HospitalGoodsResponse>(mappings.ToArray());
        }
    }
}
