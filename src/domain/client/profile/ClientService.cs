using domain.client.profile.entity;
using foundation.config;
using foundation.ef5.poco;
using irespository.client;
using irespository.client.model;
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

        public async Task<int> DeleteAsync(int id)
        {
            await _mediator.SendAsync(new DeletingClient { Id = id });
            return id; 
        }
        public async Task<Client> UpdateAsync(UpdatingClient updated)
        {
            return await _mediator.RequestAsync<UpdatingClient, Client>(updated);
        }
        public async Task<Client> CreateAsync(CreatingClient created)
        {
            return await _mediator.RequestAsync<CreatingClient, Client>(created);
        }
    }
}
