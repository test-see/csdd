using domain.client;
using foundation.ef5.poco;
using irespository.client.model;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class CreateClientRequestHandler : IRequestHandler<CreateClientRequest, Client>
    {
        private readonly ClientService _clientContext;
        public CreateClientRequestHandler(ClientService clientContext)
        {
            _clientContext = clientContext;
        }
        public async Task<Client> Handle(IReceiveContext<CreateClientRequest> context, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_clientContext.Create(context.Message));
        }
    }
}
