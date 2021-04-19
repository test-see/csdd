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

        public async Task<ClientGoods> UpdateAsync(UpdateClientGoodsRequest updated)
        {
            return await _mediator.RequestSingleAsync<UpdateClientGoodsRequest, ClientGoods>(updated);
        }
        public async Task<ClientGoods> CreateAsync(CreateClientGoodsRequest created)
        {
            return await _mediator.RequestSingleAsync<CreateClientGoodsRequest, ClientGoods>(created);
        }
        public async Task DeleteAsync(DeleteClientGoodsCommand deleted)
        {
            await _mediator.SendAsync(deleted);
        }
    }
}
