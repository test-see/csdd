using domain.client;
using foundation.ef5.poco;
using irespository.client.model;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class CreateClientRequestHandler : IRequestHandler<CreateClientRequest, Client>
    {
        private readonly ClientContext _clientContext;
        public CreateClientRequestHandler(ClientContext clientContext)
        {
            _clientContext = clientContext;
        }
        public Task<Client> Handle(IReceiveContext<CreateClientRequest> context, CancellationToken cancellationToken)
        {
            return Task.FromResult(_clientContext.Create(context.Message));
        }
    }
}
