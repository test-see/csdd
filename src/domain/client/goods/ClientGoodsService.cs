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
            return await _mediator.RequestAsync<StorageRequest<UpdateClientGoods>, ClientGoods>(new StorageRequest<UpdateClientGoods>(updated));
        }
        public async Task<ClientGoods> CreateAsync(CreateClientGoods created)
        {
            return await _mediator.RequestAsync<StorageRequest<CreateClientGoods>, ClientGoods>(new StorageRequest<CreateClientGoods>(created));
        }
        public async Task DeleteAsync(DeleteClientGoods deleted)
        {
            await _mediator.SendAsync(new StorageCommand<DeleteClientGoods>(deleted));
        }
    }
}
