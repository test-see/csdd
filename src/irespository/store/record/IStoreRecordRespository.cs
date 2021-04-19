using foundation.config;
using foundation.ef5.poco;
using irespository.store.profile.model;
using irespository.store.record.model;
using System.Threading.Tasks;

namespace irespository.store
{
    public interface IStoreRecordRespository
    {
        Task<PagerResult<StoreRecordListApiModel>> GetPagerListAsync(PagerQuery<StoreRecordListQueryModel> query);
        Task<StoreRecord> CreateAsync(StoreRecordCreateApiModel created, int userId);
        int GetConsumeAmount(int deparmentId, int goodsId, int days);
    }
}
