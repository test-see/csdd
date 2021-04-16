using domain.client;
using domain.client.profile.entity;
using foundation.ef5.poco;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class CreateClientPipeRequestHandler : IRequestHandler<Pipe<CreateClient>, Client>
    {
        private readonly ClientService _clientContext;
        public CreateClientPipeRequestHandler(ClientService clientContext)
        {
            _clientContext = clientContext;
        }
        public async Task<Client> Handle(IReceiveContext<Pipe<CreateClient>> context, CancellationToken cancellationToken)
        {
            return await _clientContext.CreateAsync(context.Message.Payload);
        }
    }
}
