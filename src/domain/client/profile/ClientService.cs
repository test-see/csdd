using domain.client.profile.entity;
using foundation.ef5.poco;
using foundation.mediator;
using Mediator.Net;
using System.Threading.Tasks;

namespace domain.client
{
    public class ClientService
    {
        private readonly IMediator _mediator;
        public ClientService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Client> UpdateAsync(UpdateClient updated)
        {
            return await _mediator.RequestAsync<StorageRequest<UpdateClient>, Client>(new StorageRequest<UpdateClient>(updated));
        }

        public async Task<Client> CreateAsync(CreateClient created)
        {
            return await _mediator.RequestAsync<StorageRequest<CreateClient>, Client>(new StorageRequest<CreateClient>(created));
        }
        public async Task DeleteAsync(DeleteClient deleted)
        {
            await _mediator.SendAsync(new StorageCommand<DeleteClient>(deleted));
        }
    }
}
