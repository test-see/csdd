using domain.client;
using domain.client.profile.entity;
using domain.sys;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class DeleteConfigPipeCommandHandler : ICommandHandler<Pipe<DeleteConfig>>
    {
        private readonly ConfigService _clientContext;
        public DeleteConfigPipeCommandHandler(ConfigService clientContext)
        {
            _clientContext = clientContext;
        }
        public async Task Handle(IReceiveContext<Pipe<DeleteConfig>> context, CancellationToken cancellationToken)
        {
            await _clientContext.DeleteAsync(context.Message.Payload);
        }
    }
}
