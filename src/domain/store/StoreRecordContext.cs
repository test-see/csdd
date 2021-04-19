using foundation.config;
using irespository.store;
using irespository.store.profile.model;
using System.Threading.Tasks;

namespace domain.store
{
    public class StoreRecordContext
    {
        private readonly IStoreRecordRespository _storeRecordRespository;
        public StoreRecordContext(IStoreRecordRespository storeRecordRespository)
        {
            _storeRecordRespository = storeRecordRespository;
        }

        public async Task<PagerResult<StoreRecordListApiModel>> GetPagerListAsync(PagerQuery<StoreRecordListQueryModel> query)
        {
            return await _storeRecordRespository.GetPagerListAsync(query);
        }

        public int GetConsumeAmount(int deparmentId, int goodsId, int days)
        {
            return _storeRecordRespository.GetConsumeAmount(deparmentId, goodsId, days);
        }
    }
}
