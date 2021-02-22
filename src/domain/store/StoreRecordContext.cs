using foundation.config;
using irespository.store;
using irespository.store.profile.model;

namespace domain.store
{
    public class StoreRecordContext
    {
        private readonly IStoreRecordRespository _storeRecordRespository;
        public StoreRecordContext(IStoreRecordRespository storeRecordRespository)
        {
            _storeRecordRespository = storeRecordRespository;
        }

        public PagerResult<StoreRecordListApiModel> GetPagerList(PagerQuery<StoreRecordListQueryModel> query)
        {
            return _storeRecordRespository.GetPagerList(query);
        }

        public int GetAA()
        {
            return 0;
        }
    }
}
