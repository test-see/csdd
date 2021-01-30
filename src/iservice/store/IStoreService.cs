using foundation.config;
using foundation.ef5.poco;
using irespository.store.model;
using irespository.store.profile.model;

namespace iservice.store
{
    public interface IStoreService
    {
        PagerResult<StoreListApiModel> GetPagerList(PagerQuery<StoreListQueryModel> query);
        Store CustomizeCreate(CustomizeStoreChangeApiModel created, int department, int userId);
    }
}
