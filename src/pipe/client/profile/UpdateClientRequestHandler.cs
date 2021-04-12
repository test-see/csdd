using domain.client;
using domain.client.profile.entity;
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
        private readonly ClientService _clientContext;
        public UpdateClientRequestHandler(ClientService clientContext)
        {
            _clientContext = clientContext;
        }
        public async Task<Client> Handle(IReceiveContext<UpdateClientRequest> context, CancellationToken cancellationToken)
        {
            var updating = new ClientUpdating
            {
                Id = context.Message.Id,
                Name = context.Message.Name,
                UserId = context.Message.UserId,
            };
            return await _clientContext.UpdateAsync(updating);
        }
    }
}
