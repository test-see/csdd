using foundation.config;
using foundation.ef5.poco;
using irespository.store.inout.profile.enums;
using irespository.storeinout.model;

namespace irespository.storeinout
{
    public interface IStoreInoutRespository
    {
        PagerResult<StoreInoutListApiModel> GetPagerList(PagerQuery<StoreInoutListQueryModel> query);
        StoreInout Create(StoreInoutCreateApiModel created, int departmentId, int userId);
        int Delete(int id);
        int Update(int id, StoreInoutUpdateApiModel updated);
        int UpdateStatus(int id, StoreInoutStatus status);
        StoreInout Get(int id);
    }
}
