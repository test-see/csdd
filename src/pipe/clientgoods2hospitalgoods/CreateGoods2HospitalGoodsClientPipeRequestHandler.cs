using domain.client;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.client.goods.model;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class CreateClientGoods2HospitalGoodsPipeRequestHandler : IRequestHandler<PipeRequest<CreateClientGoods2HospitalGoods>, ClientGoods2HospitalGoods>
    {
        private readonly ClientGoods2HospitalGoodsService _clientContext;
        public CreateClientGoods2HospitalGoodsPipeRequestHandler(ClientGoods2HospitalGoodsService clientContext)
        {
            _clientContext = clientContext;
        }
        public async Task<ClientGoods2HospitalGoods> Handle(IReceiveContext<PipeRequest<CreateClientGoods2HospitalGoods>> context, CancellationToken cancellationToken)
        {
            return await _clientContext.CreateAsync(context.Message.Payload);
        }
    }
}
