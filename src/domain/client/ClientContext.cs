using foundation.config;
using foundation.ef5.poco;
using irespository.client;
using irespository.client.model;

namespace domain.client
{
    public class ClientContext
    {
        private readonly IClientRespository _ClientRespository;
        public ClientContext(IClientRespository ClientRespository)
        {
            _ClientRespository = ClientRespository;
        }

        public PagerResult<ClientListApiModel> GetPagerList(PagerQuery<ClientListQueryModel> query)
        {
            return _ClientRespository.GetPagerList(query);
        }
        public Client Create(ClientCreateApiModel created, int userId)
        {
            return _ClientRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _ClientRespository.Delete(id);
        }
        public int Update(int id, ClientUpdateApiModel updated)
        {
            return _ClientRespository.Update(id, updated);
        }
    }
}
