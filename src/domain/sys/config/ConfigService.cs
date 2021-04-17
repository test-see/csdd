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

        public async Task<SysConfig> CreateAsync(CreateConfig created)
        {
            return await _mediator.RequestSingleAsync<CreateConfig, SysConfig>(created);
        }
        public async Task DeleteAsync(DeleteConfig deleted)
        {
            await _mediator.SendAsync(deleted);
        }
    }
}
