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
        public async Task<Client> CreateAsync(CreateClient2HospitalClient created)
        {
            return await _mediator.RequestAsync<StorageRequest<CreateClient2HospitalClient>, Client>(new StorageRequest<CreateClient2HospitalClient>(created));
        }
        public async Task DeleteAsync(DeleteClient2HospitalClient deleted)
        {
            await _mediator.SendAsync(new StorageCommand<DeleteClient2HospitalClient>(deleted));
        }
    }
}
