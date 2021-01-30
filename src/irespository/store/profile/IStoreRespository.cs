using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;
using irespository.store.profile.model;

namespace irespository.store
{
    public interface IStoreRespository
    {
        PagerResult<StoreListApiModel> GetPagerList(PagerQuery<StoreListQueryModel> query);
        Store CreateOrUpdate(StoreUpdateApiModel updated, int department, int userId);
        Store GetIndexByGoods(int department, int goods);
    }
}
