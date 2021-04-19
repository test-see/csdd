using domain.client;
using domain.client.profile.entity;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class DeleteClientPipeCommandHandler : ICommandHandler<Pipe<DeleteClientCommand>>
    {
        private readonly ClientService _clientContext;
        public DeleteClientPipeCommandHandler(ClientService clientContext)
        {
            _clientContext = clientContext;
        }
        public async Task Handle(IReceiveContext<Pipe<DeleteClientCommand>> context, CancellationToken cancellationToken)
        {
            await _clientContext.DeleteAsync(context.Message.Payload);
        }
    }
}
