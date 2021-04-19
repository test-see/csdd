using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;
using irespository.purchase.profile.enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace irespository.purchase
{
    public interface IPurchaseRespository
    {
        Task<PagerResult<PurchaseListApiModel>> GetPagerListAsync(PagerQuery<PurchaseListQueryModel> query);
        Purchase Create(PurchaseCreateApiModel created, int departmentId, int userId);
        int Delete(int id);
        int Update(int id, PurchaseUpdateApiModel updated);
        Task<PurchaseIndexApiModel> GetIndexAsync(int id);
        int UpdateStatus(int id, PurchaseStatus status);
        Task<IList<PurchaseValueModel>> GetValueAsync(int[] ids);
    }
}
