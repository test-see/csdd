using foundation.config;
using foundation.ef5.poco;
using irespository.storeinout.model;
using System.Collections.Generic;

namespace iservice.store
{
    public interface IStoreInoutService
    {
        PagerResult<StoreInoutListApiModel> GetPagerList(PagerQuery<StoreInoutListQueryModel> query);
        StoreInout Create(StoreInoutCreateApiModel created, int departmentId, int userId);
        int Delete(int id);
        int Update(int id, StoreInoutUpdateApiModel updated);
        int Submit(int id, int userId);
        IEnumerable<DataStoreChangeType> GetCustomizeChangeTypeList();
        StoreInoutIndexApiModel GetIndex(int id);
    }
}
