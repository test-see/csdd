using domain.client;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using nouns.client.profile;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class DeleteClientCommandHandler : ICommandHandler<DeleteClientCommand>
    {
        private readonly ClientService _clientContext;
        public DeleteClientCommandHandler(ClientService clientContext)
        {
            _clientContext = clientContext;
        }
        public Task Handle(IReceiveContext<DeleteClientCommand> context, CancellationToken cancellationToken)
        {
            _clientContext.Delete(context.Message.Id);
            return Task.FromResult(0);
        }
    }
}
