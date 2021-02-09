using foundation.config;
using foundation.ef5.poco;
using irespository.store.profile.model;
using irespository.store.record.model;

namespace irespository.store
{
    public interface IStoreRecordRespository
    {
        PagerResult<StoreRecordListApiModel> GetPagerList(PagerQuery<StoreRecordListQueryModel> query);
        StoreRecord Create(StoreRecordCreateApiModel created, int userId);
    }
}
