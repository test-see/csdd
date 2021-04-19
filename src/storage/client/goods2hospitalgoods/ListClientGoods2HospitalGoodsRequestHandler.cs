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
    public class ListClientGoods2HospitalGoodsRequestHandler : IRequestHandler<ListClientGoods2HospitalGoodsRequest, ListResponse<ListClientGoods2HospitalGoodsResponse>>
    {
        private readonly DefaultDbContext _context;
        private readonly IMediator _mediator;
        public ListClientGoods2HospitalGoodsRequestHandler(DefaultDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ListResponse<ListClientGoods2HospitalGoodsResponse>> Handle(IReceiveContext<ListClientGoods2HospitalGoodsRequest> context, CancellationToken cancellationToken)
        {
            var payload = context.Message;
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

            var goods = await _mediator.ListByIdsAsync<GetHospitalGoodsRequest, GetHospitalGoodsResponse>(mappings.Select(x => x.HospitalGoods.Id).ToList());

            foreach (var m in mappings)
            {
                m.HospitalGoods = goods.FirstOrDefault(x => x.Id == m.HospitalGoods.Id);
            }
            return mappings.ToResponse();
        }
    }
}
