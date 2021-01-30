using foundation.config;
using foundation.ef5.poco;
using irespository.store;
using irespository.store.model;
using irespository.store.profile.model;

namespace domain.store
{
    public class StoreContext
    {
        private readonly IStoreRespository _storeRespository;
        public StoreContext(IStoreRespository storeRespository)
        {
            _storeRespository = storeRespository;
        }

        public PagerResult<StoreListApiModel> GetPagerList(PagerQuery<StoreListQueryModel> query)
        {
            return _storeRespository.GetPagerList(query);
        }

        public Store CustomizeCreate(CustomizeStoreCreateApiModel created, int userId)
        {
            return _storeRespository.CustomizeCreate(created, userId);
        }


    }
}
