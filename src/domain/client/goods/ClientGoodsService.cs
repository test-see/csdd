using domain.client.profile.entity;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.client.goods.model;
using Mediator.Net;
using System.Threading.Tasks;

namespace domain.client
{
    public class ClientGoodsService
    {
        private readonly IMediator _mediator;
        public ClientGoodsService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ClientGoods> UpdateAsync(UpdateClientGoods updated)
        {
            return await _mediator.RequestSingleAsync<UpdateClientGoods, ClientGoods>(updated);
        }
        public async Task<ClientGoods> CreateAsync(CreateClientGoods created)
        {
            return await _mediator.RequestSingleAsync<CreateClientGoods, ClientGoods>(created);
        }
        public async Task DeleteAsync(DeleteClientGoods deleted)
        {
            await _mediator.SendStorageAsync(deleted);
        }
    }
}
