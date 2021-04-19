using domain.client.profile.entity;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.client.maping.model;
using Mediator.Net;
using System.Threading.Tasks;

namespace domain.client
{
    public class Client2HospitalClientService
    {
        private readonly IMediator _mediator;
        public Client2HospitalClientService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Client2HospitalClient> CreateAsync(CreateClient2HospitalClientRequest created)
        {
            return await _mediator.RequestAsync<CreateClient2HospitalClientRequest, Client2HospitalClient>(created);
        }
        public async Task DeleteAsync(DeleteClient2HospitalClientCommand deleted)
        {
            await _mediator.SendAsync(deleted);
        }
    }
}
