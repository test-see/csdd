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
    public class CreateWhitePhonePipeRequestHandler : IRequestHandler<Pipe<CreateWhitePhoneRequest>, SysWhitePhone>
    {
        private readonly WhitePhoneService _clientContext;
        public CreateWhitePhonePipeRequestHandler(WhitePhoneService clientContext)
        {
            _clientContext = clientContext;
        }
        public async Task<SysWhitePhone> Handle(IReceiveContext<Pipe<CreateWhitePhoneRequest>> context, CancellationToken cancellationToken)
        {
            return await _clientContext.CreateAsync(context.Message.Payload);
        }
    }
}
