using domain.store;
using foundation.config;
using foundation.ef5.poco;
using irespository.store.model;
using irespository.store.profile.model;
using iservice.store;
using System.Collections.Generic;

namespace service.store
{
    public class StoreService : IStoreService
    {
        private readonly StoreContext _storeContext;
        public StoreService(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public PagerResult<StoreListApiModel> GetPagerList(PagerQuery<StoreListQueryModel> query)
        {
            return _storeContext.GetPagerList(query);
        }
        public Store GetIndexByGoods(int department, int goods)
        {
            return _storeContext.GetIndexByGoods(department, goods);
        }
    }
}
