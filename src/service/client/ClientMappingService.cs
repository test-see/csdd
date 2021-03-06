using domain.client;
using foundation.config;
using foundation.ef5.poco;
using irespository.client.maping.model;
using irespository.client.maping.profile.model;
using iservice.client;

namespace service.client
{
    public class ClientMappingService: IClientMappingService
    {
        private readonly ClientMappingContext _clientMappingContext;
        public ClientMappingService(ClientMappingContext clientMappingContext)
        {
            _clientMappingContext = clientMappingContext;
        }
        public PagerResult<ClientMappingListApiModel> GetPagerList(PagerQuery<ClientMappingListQueryModel> query)
        {
            return _clientMappingContext.GetPagerList(query);
        }
        public ClientMapping Create(ClientMappingCreateApiModel created, int userId)
        {
            return _clientMappingContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _clientMappingContext.Delete(id);
        }
    }
}
