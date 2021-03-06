using foundation.config;
using foundation.ef5.poco;
using irespository.client.maping;
using irespository.client.maping.model;
using irespository.client.maping.profile.model;

namespace domain.client
{
    public class ClientMappingContext
    {
        private readonly IClientMappingRespository _clientMappingRespository;
        public ClientMappingContext(IClientMappingRespository clientMappingRespository)
        {
            _clientMappingRespository = clientMappingRespository;
        }
        public PagerResult<ClientMappingListApiModel> GetPagerList(PagerQuery<ClientMappingListQueryModel> query)
        {
            return _clientMappingRespository.GetPagerList(query);
        }
        public ClientMapping Create(ClientMappingCreateApiModel created, int userId)
        {
            return _clientMappingRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _clientMappingRespository.Delete(id);
        }
    }
}
