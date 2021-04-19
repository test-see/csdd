using foundation.config;
using foundation.ef5.poco;
using irespository.store.model;
using irespository.store.profile.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iservice.store
{
    public interface IStoreService
    {
        Task<PagerResult<StoreListApiModel>> GetPagerListAsync(PagerQuery<StoreListQueryModel> query);
        Store GetIndexByGoods(int department, int goods);
    }
}
