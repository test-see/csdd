using domain.store;
using foundation.config;
using irespository.store.profile.model;
using iservice.store;
using System.Threading.Tasks;

namespace service.store
{
    public class StoreRecordService : IStoreRecordService
    {
        private readonly StoreRecordContext _storeRecordContext;
        public StoreRecordService(StoreRecordContext storeRecordContext)
        {
            _storeRecordContext = storeRecordContext;
        }
        public async Task<PagerResult<StoreRecordListApiModel>> GetPagerListAsync(PagerQuery<StoreRecordListQueryModel> query)
        {
            return await _storeRecordContext.GetPagerListAsync(query);
        }

    }
}
