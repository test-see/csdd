using domain.client;
using foundation.ef5.poco;
using irespository.client.model;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class UpdateClientRequestHandler : IRequestHandler<UpdateClientRequest, Client>
    {
        private readonly ClientContext _clientContext;
        public UpdateClientRequestHandler(ClientContext clientContext)
        {
            _clientContext = clientContext;
        }
        public Task<Client> Handle(IReceiveContext<UpdateClientRequest> context, CancellationToken cancellationToken)
        {
            return Task.FromResult(_clientContext.Update(context.Message));
        }
    }
}
