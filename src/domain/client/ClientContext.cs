using foundation.config;
using foundation.ef5.poco;
using irespository.client;
using irespository.client.model;

namespace domain.client
{
    public class ClientContext
    {
        private readonly IClientRespository _clientRespository;
        public ClientContext(IClientRespository clientRespository)
        {
            _clientRespository = clientRespository;
        }

        public PagerResult<ListClientResponse> GetPagerList(PagerQuery<ListClientRequest> query)
        {
            return _clientRespository.GetPagerList(query);
        }
        public Client Create(ClientCreateApiModel created, int userId)
        {
            return _clientRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _clientRespository.Delete(id);
        }
        public int Update(int id, ClientUpdateApiModel updated, int userId)
        {
            return _clientRespository.Update(id, updated, userId);
        }

        public ClientIndexApiModel GetIndex(int id)
        {
            return _clientRespository.GetIndex(id);
        }

    }
}
