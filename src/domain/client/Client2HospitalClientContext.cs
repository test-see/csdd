using foundation.config;
using foundation.ef5.poco;
using irespository.client.maping;
using irespository.client.maping.model;
using irespository.client.maping.profile.model;

namespace domain.client
{
    public class Client2HospitalClientContext
    {
        private readonly IClient2HospitalClientRespository _clientMappingRespository;
        public Client2HospitalClientContext(IClient2HospitalClientRespository clientMappingRespository)
        {
            _clientMappingRespository = clientMappingRespository;
        }
        public PagerResult<ListClient2HospitalClientResponse> GetPagerList(PagerQuery<Client2HospitalClientListQueryModel> query)
        {
            return _clientMappingRespository.GetPagerList(query);
        }
        public Client2HospitalClient Create(Client2HospitalClientCreateApiModel created, int userId)
        {
            return _clientMappingRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _clientMappingRespository.Delete(id);
        }
    }
}
