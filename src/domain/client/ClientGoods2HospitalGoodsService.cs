using domain.client.goods2hospitalgoods.entity;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.client.goods.model;
using Mediator.Net;
using System.Threading.Tasks;

namespace domain.client
{
    public class ClientGoods2HospitalGoodsService
    {
        private readonly IMediator _mediator;
        public ClientGoods2HospitalGoodsService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<ClientGoods2HospitalGoods> CreateAsync(CreateClientGoods2HospitalGoodsRequest created)
        {
            return await _mediator.RequestAsync<CreateClientGoods2HospitalGoodsRequest, ClientGoods2HospitalGoods>(created);
        }

        public async Task DeleteAsync(DeleteClientGoods2HospitalGoodsCommand deleted)
        {
            await _mediator.SendAsync(deleted);
        }
    }
}
