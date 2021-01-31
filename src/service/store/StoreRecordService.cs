using domain.store;
using foundation.config;
using irespository.store.profile.model;
using iservice.store;

namespace service.store
{
    public class StoreRecordService : IStoreRecordService
    {
        private readonly StoreRecordContext _storeRecordContext;
        public StoreRecordService(StoreRecordContext storeRecordContext)
        {
            _storeRecordContext = storeRecordContext;
        }
        public PagerResult<StoreRecordListApiModel> GetPagerList(PagerQuery<StoreRecordListQueryModel> query)
        {
            return _storeRecordContext.GetPagerList(query);
        }

    }
}
