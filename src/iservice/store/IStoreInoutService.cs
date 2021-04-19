using foundation.config;
using foundation.ef5.poco;
using irespository.storeinout.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iservice.store
{
    public interface IStoreInoutService
    {
        Task<PagerResult<StoreInoutListApiModel>> GetPagerListAsync(PagerQuery<StoreInoutListQueryModel> query);
        StoreInout Create(StoreInoutCreateApiModel created, int departmentId, int userId);
        int Delete(int id);
        int Update(int id, StoreInoutUpdateApiModel updated);
        Task<int> SubmitAsync(int id, int userId);
        IEnumerable<DataStoreChangeType> GetCustomizeChangeTypeList();
        Task<StoreInoutIndexApiModel> GetIndexAsync(int id);
    }
}
