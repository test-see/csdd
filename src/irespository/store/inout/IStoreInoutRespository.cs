using foundation.config;
using foundation.ef5.poco;
using irespository.store.inout.profile.enums;
using irespository.storeinout.model;
using System.Threading.Tasks;

namespace irespository.storeinout
{
    public interface IStoreInoutRespository
    {
        Task<PagerResult<StoreInoutListApiModel>> GetPagerListAsync(PagerQuery<StoreInoutListQueryModel> query);
        StoreInout Create(StoreInoutCreateApiModel created, int departmentId, int userId);
        int Delete(int id);
        int Update(int id, StoreInoutUpdateApiModel updated);
        int UpdateStatus(int id, StoreInoutStatus status);
        Task<StoreInoutIndexApiModel> GetIndexAsync(int id);
    }
}
