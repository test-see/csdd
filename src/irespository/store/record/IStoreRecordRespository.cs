using foundation.config;
using irespository.store.profile.model;

namespace irespository.store
{
    public interface IStoreRecordRespository
    {
        PagerResult<StoreRecordListApiModel> GetPagerList(PagerQuery<StoreRecordListQueryModel> query);    
    }
}
