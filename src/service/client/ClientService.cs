using domain.client;
using foundation.config;
using foundation.ef5.poco;
using irespository.client.model;
using iservice.client;

namespace service.client
{
    public class ClientService : IClientService
    {
        private readonly ClientContext _clientContext;
        public ClientService(ClientContext clientContext)
        {
            _clientContext = clientContext;
        }
        public ClientIndexApiModel GetIndex(int id)
        {
            return _clientContext.GetIndex(id);
        }
    }
}
