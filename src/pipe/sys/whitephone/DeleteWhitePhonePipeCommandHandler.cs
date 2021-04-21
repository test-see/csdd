using domain.client.profile.entity;
using domain.sys;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class DeleteWhitePhonePipeCommandHandler : ICommandHandler<Pipe<DeleteWhitePhoneCommand>>
    {
        private readonly WhitePhoneService _clientContext;
        public DeleteWhitePhonePipeCommandHandler(WhitePhoneService clientContext)
        {
            _clientContext = clientContext;
        }
        public async Task Handle(IReceiveContext<Pipe<DeleteWhitePhoneCommand>> context, CancellationToken cancellationToken)
        {
            await _clientContext.DeleteAsync(context.Message.Payload);
        }
    }
}
