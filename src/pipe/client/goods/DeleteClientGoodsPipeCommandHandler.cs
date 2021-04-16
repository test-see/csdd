using domain.client;
using domain.client.profile.entity;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class DeleteClientGoodsPipeCommandHandler : ICommandHandler<Pipe<DeleteClientGoods>>
    {
        private readonly ClientGoodsService _clientContext;
        public DeleteClientGoodsPipeCommandHandler(ClientGoodsService clientContext)
        {
            _clientContext = clientContext;
        }
        public async Task Handle(IReceiveContext<Pipe<DeleteClientGoods>> context, CancellationToken cancellationToken)
        {
            await _clientContext.DeleteAsync(context.Message.Payload);
        }
    }
}
