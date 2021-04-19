using domain.store;
using foundation.config;
using foundation.ef5.poco;
using irespository.store.model;
using irespository.store.profile.model;
using iservice.store;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace service.store
{
    public class StoreService : IStoreService
    {
        private readonly StoreContext _storeContext;
        public StoreService(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task<PagerResult<StoreListApiModel>> GetPagerListAsync(PagerQuery<StoreListQueryModel> query)
        {
            return await _storeContext.GetPagerListAsync(query);
        }
        public Store GetIndexByGoods(int department, int goods)
        {
            return _storeContext.GetIndexByGoods(department, goods);
        }
    }
}
