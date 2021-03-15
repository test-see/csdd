using foundation.config;
using foundation.ef5.poco;
using irespository.store.model;
using irespository.store.profile.model;
using System.Collections.Generic;

namespace irespository.store
{
    public interface IStoreRespository
    {
        PagerResult<StoreListApiModel> GetPagerList(PagerQuery<StoreListQueryModel> query);
        int CreateOrUpdate(StoreChangeGoodsValueModel created, int changeTypeId, int departmentId, int userId);
        Store GetIndexByGoods(int department, int goods);
        IList<StoreListApiModel> GetListByDepartment(int departmentId);


    }
}
