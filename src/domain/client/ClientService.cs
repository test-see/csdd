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

        public async Task<Client> UpdateAsync(UpdateClientRequest updated)
        {
            return await _mediator.RequestAsync<UpdateClientRequest, Client>(updated);
        }

        public async Task<Client> CreateAsync(CreateClientRequest created)
        {
            return await _mediator.RequestAsync<CreateClientRequest, Client>(created);
        }
        public async Task DeleteAsync(DeleteClientCommand deleted)
        {
            await _mediator.SendAsync(deleted);
        }
    }
}
