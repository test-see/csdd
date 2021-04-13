using domain.client.profile.entity;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.client;
using Mediator.Net;
using nouns.client.profile;
using System.Threading.Tasks;

namespace domain.client
{
    public class ClientService
    {
        private readonly IClientRespository _clientRespository;
        private readonly IMediator _mediator;
        public ClientService(IClientRespository clientRespository, IMediator mediator)
        {
            _clientRespository = clientRespository;
            _mediator = mediator;
        }

        public GetClientResponse GetIndex(int id)
        {
            return _clientRespository.GetIndex(id);
        }

        public async Task<Client> UpdateAsync(UpdatingClient updated)
        {
            return await _mediator.RequestAsync<UpdatingClient, Client>(updated);
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
