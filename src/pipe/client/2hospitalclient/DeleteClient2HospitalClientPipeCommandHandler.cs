using domain.client;
using domain.client.profile.entity;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class DeleteClient2HospitalClientPipeCommandHandler : ICommandHandler<Pipe<DeleteClient2HospitalClient>>
    {
        private readonly Client2HospitalClientService _clientContext;
        public DeleteClient2HospitalClientPipeCommandHandler(Client2HospitalClientService clientContext)
        {
            _clientContext = clientContext;
        }
        public async Task Handle(IReceiveContext<Pipe<DeleteClient2HospitalClient>> context, CancellationToken cancellationToken)
        {
            await _clientContext.DeleteAsync(context.Message.Payload);
        }
    }
}
