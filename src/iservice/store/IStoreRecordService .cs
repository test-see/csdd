using foundation.config;
using irespository.store.profile.model;
using System.Threading.Tasks;

namespace iservice.store
{
    public interface IStoreRecordService
    {
        Task<PagerResult<StoreRecordListApiModel>> GetPagerListAsync(PagerQuery<StoreRecordListQueryModel> query);
    }
}
