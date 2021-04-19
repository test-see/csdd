using foundation.config;
using foundation.ef5.poco;
using irespository.store.model;
using irespository.store.profile.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace irespository.store
{
    public interface IStoreRespository
    {
        Task<PagerResult<StoreListApiModel>> GetPagerListAsync(PagerQuery<StoreListQueryModel> query);
        int CreateOrUpdate(StoreChangeGoodsValueModel created, int afterQty, int changeTypeId, int departmentId, int userId);
        Store GetIndexByGoods(int department, int goods);
        Task<IList<StoreListApiModel>> GetListByDepartmentAsync(int departmentId);


    }
}
