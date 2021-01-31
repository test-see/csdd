using foundation.config;
using irespository.store.profile.model;

namespace iservice.store
{
    public interface IStoreRecordService
    {
        PagerResult<StoreRecordListApiModel> GetPagerList(PagerQuery<StoreRecordListQueryModel> query);
    }
}
