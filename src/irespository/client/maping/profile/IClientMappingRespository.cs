using foundation.config;
using foundation.ef5.poco;
using irespository.client.maping.model;
using irespository.client.maping.profile.model;

namespace irespository.client.maping
{
    public interface IClientMappingRespository
    {
        int Delete(int id);
        ClientMapping Create(ClientMappingCreateApiModel created, int userId);
        PagerResult<ClientMappingListApiModel> GetPagerList(PagerQuery<ClientMappingListQueryModel> query);
    }
}
