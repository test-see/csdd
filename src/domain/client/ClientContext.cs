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
        public Client Create(CreateClientRequest created)
        {
            return _clientRespository.Create(created);
        }
        public int Delete(int id)
        {
            return _clientRespository.Delete(id);
        }
        public Client Update(UpdateClientRequest updated)
        {
            return _clientRespository.Update(updated.Id, updated, updated.UserId);
        }

        public ClientIndexApiModel GetIndex(int id)
        {
            return _clientRespository.GetIndex(id);
        }

    }
}
