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
        public async Task Handle(IReceiveContext<DeleteClientCommand> context, CancellationToken cancellationToken)
        {
            await _clientContext.DeleteAsync(context.Message.Id);
        }
    }
}
