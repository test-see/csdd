using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iservice.purchase
{
    public interface IPurchaseGoodsBillnoService
    {
        public Task<PagerResult<PurchaseGoodsBillnoListApiModel>> GetPagerListByHospitalAsync(PagerQuery<PurchaseGoodsBillnoListQueryModel> query, int hospitalDepartmentId);
        Task<PagerResult<PurchaseGoodsBillnoListApiModel>> GetPagerListByClientAsync(PagerQuery<PurchaseGoodsBillnoListQueryModel> query, int clientId);
        PurchaseGoodsBillno Create(PurchaseGoodsBillnoCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, PurchaseGoodsBillnoUpdateApiModel updated);
        Task<int> ComfirmAsync(IList<int> ids, int userId);
    }
}
