using foundation.config;
using foundation.ef5.poco;
using irespository.client.maping.model;
using irespository.client.maping.profile.model;

namespace iservice.client
{
    public interface IClientMappingService
    {
        ClientMapping Create(ClientMappingCreateApiModel created, int userId);
        int Delete(int id);
        PagerResult<ClientMappingListApiModel> GetPagerList(PagerQuery<ClientMappingListQueryModel> query);
    }
}
