using domain.client.profile.entity;
using foundation.ef5.poco;
using foundation.mediator;
using Mediator.Net;
using System.Threading.Tasks;

namespace domain.sys
{
    public class ConfigService
    {
        private readonly IMediator _mediator;
        public ConfigService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<SysConfig> CreateAsync(CreateConfigRequest created)
        {
            return await _mediator.RequestSingleAsync<CreateConfigRequest, SysConfig>(created);
        }
        public async Task DeleteAsync(DeleteConfigCommand deleted)
        {
            await _mediator.SendAsync(deleted);
        }
    }
}
