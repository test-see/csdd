using domain.client;
using foundation.config;
using foundation.ef5.poco;
using irespository.client.model;
using iservice.client;

namespace service.client
{
    public class ClientService : IClientService
    {
        private readonly ClientContext _ClientContext;
        public ClientService(ClientContext ClientContext)
        {
            _ClientContext = ClientContext;
        }
        public PagerResult<ClientListApiModel> GetPagerList(PagerQuery<ClientListQueryModel> query)
        {
            return _ClientContext.GetPagerList(query);
        }
        public Client Create(ClientCreateApiModel created, int userId)
        {
            return _ClientContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _ClientContext.Delete(id);
        }

        public int Update(int id, ClientUpdateApiModel updated, int userId)
        {
            return _ClientContext.Update(id, updated, userId);
        }
    }
}
