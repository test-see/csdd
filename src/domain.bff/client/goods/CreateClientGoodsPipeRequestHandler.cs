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
    public class CreateClientGoodsPipeRequestHandler : IRequestHandler<Pipe<CreateClientGoodsRequest>, ClientGoods>
    {
        private readonly ClientGoodsService _clientContext;
        public CreateClientGoodsPipeRequestHandler(ClientGoodsService clientContext)
        {
            _clientContext = clientContext;
        }
        public async Task<ClientGoods> Handle(IReceiveContext<Pipe<CreateClientGoodsRequest>> context, CancellationToken cancellationToken)
        {
            return await _clientContext.CreateAsync(context.Message.Payload);
        }
    }
}
