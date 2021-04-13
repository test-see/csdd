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
        public async Task<ClientGoods2HospitalGoods> CreateAsync(CreateClientGoods2HospitalGoods created)
        {
            return await _mediator.RequestAsync<StorageRequest<CreateClientGoods2HospitalGoods>, ClientGoods2HospitalGoods>(new StorageRequest<CreateClientGoods2HospitalGoods>(created));
        }

        public async Task DeleteAsync(DeleteClientGoods2HospitalGoods deleted)
        {
            await _mediator.SendAsync(new StorageCommand<DeleteClientGoods2HospitalGoods>(deleted));
        }
    }
}
