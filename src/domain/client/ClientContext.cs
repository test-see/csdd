using foundation.config;
using foundation.ef5.poco;
using irespository.client;
using irespository.client.model;
using System.Collections.Generic;

namespace domain.client
{
    public class ClientContext
    {
        private readonly IClientRespository _clientRespository;
        public ClientContext(IClientRespository clientRespository)
        {
            _clientRespository = clientRespository;
        }

        public PagerResult<ClientListApiModel> GetPagerList(PagerQuery<ClientListQueryModel> query)
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

        public IList<IdNameValueModel> GetHospitalClientList()
        {
            return _clientRespository.GetHospitalClientList();
        }
    }
}
