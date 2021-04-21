using domain.client.profile.entity;
using foundation.ef5.poco;
using Mediator.Net;
using System.Threading.Tasks;

namespace domain.sys
{
    public class WhitePhoneService
    {
        private readonly IMediator _mediator;
        public WhitePhoneService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<SysWhitePhone> CreateAsync(CreateWhitePhoneRequest created)
        {
            return await _mediator.RequestAsync<CreateWhitePhoneRequest, SysWhitePhone>(created);
        }
        public async Task DeleteAsync(DeleteWhitePhoneCommand deleted)
        {
            await _mediator.SendAsync(deleted);
        }
    }
}
