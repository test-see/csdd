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
        public Store CustomizeCreate(CustomizeStoreChangeApiModel created, int department, int userId)
        {
            return _storeContext.CustomizeCreate(created, department, userId);
        }

        public PagerResult<StoreListApiModel> GetPagerList(PagerQuery<StoreListQueryModel> query)
        {
            return _storeContext.GetPagerList(query);
        }


        public IEnumerable<DataStoreChangeType> GetCustomizeChangeTypeList()
        {
            return _storeContext.GetCustomizeChangeTypeList();
        }
    }
}
