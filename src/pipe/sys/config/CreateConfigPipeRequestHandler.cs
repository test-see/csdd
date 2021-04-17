using domain.client;
using domain.client.profile.entity;
using domain.sys;
using foundation.ef5.poco;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class CreateConfigPipeRequestHandler : IRequestHandler<Pipe<CreateConfig>, SysConfig>
    {
        private readonly ConfigService _clientContext;
        public CreateConfigPipeRequestHandler(ConfigService clientContext)
        {
            _clientContext = clientContext;
        }
        public async Task<SysConfig> Handle(IReceiveContext<Pipe<CreateConfig>> context, CancellationToken cancellationToken)
        {
            return await _clientContext.CreateAsync(context.Message.Payload);
        }
    }
}
