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
    public class UpdateClientPipeRequestHandler : IRequestHandler<PipeRequest<UpdateClient>, Client>
    {
        private readonly ClientService _clientContext;
        public UpdateClientPipeRequestHandler(ClientService clientContext)
        {
            _clientContext = clientContext;
        }
        public async Task<Client> Handle(IReceiveContext<PipeRequest<UpdateClient>> context, CancellationToken cancellationToken)
        {
            return await _clientContext.UpdateAsync(context.Message.Payload);
        }
    }
}
