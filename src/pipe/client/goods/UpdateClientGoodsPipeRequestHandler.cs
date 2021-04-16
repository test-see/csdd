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
    public class UpdateClientGoodsPipeRequestHandler : IRequestHandler<Pipe<UpdateClientGoods>, ClientGoods>
    {
        private readonly ClientGoodsService _clientContext;
        public UpdateClientGoodsPipeRequestHandler(ClientGoodsService clientContext)
        {
            _clientContext = clientContext;
        }
        public async Task<ClientGoods> Handle(IReceiveContext<Pipe<UpdateClientGoods>> context, CancellationToken cancellationToken)
        {
            return await _clientContext.UpdateAsync(context.Message.Payload);
        }
    }
}
