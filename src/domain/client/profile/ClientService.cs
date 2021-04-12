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

        public PagerResult<ListClientResponse> GetPagerList(PagerQuery<ListClientRequest> query)
        {
            return _clientRespository.GetPagerList(query);
        }

        public GetClientResponse GetIndex(int id)
        {
            return _clientRespository.GetIndex(id);
        }

        public async Task<int> DeleteAsync(int id)
        {
            await _mediator.SendAsync(new ClientDeleting { Id = id });
            return id; 
        }
        public async Task<Client> UpdateAsync(ClientUpdating updated)
        {
            return await _mediator.RequestAsync<ClientUpdating, Client>(updated);
        }
        public async Task<Client> CreateAsync(ClientCreating created)
        {
            return await _mediator.RequestAsync<ClientCreating, Client>(created);
        }
    }
}
