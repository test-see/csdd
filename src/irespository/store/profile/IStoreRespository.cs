using foundation.config;
using foundation.ef5.poco;
using irespository.store.model;
using irespository.store.profile.model;

namespace irespository.store
{
    public interface IStoreRespository
    {
        PagerResult<StoreListApiModel> GetPagerList(PagerQuery<StoreListQueryModel> query);
        Store CustomizeCreate(CustomizeStoreCreateApiModel created, int userId);
    }
}
