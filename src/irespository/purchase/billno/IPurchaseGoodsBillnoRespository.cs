using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;
using irespository.purchase.profile.enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace irespository.purchase
{
    public interface IPurchaseGoodsBillnoRespository
    {
        Task<PagerResult<PurchaseGoodsBillnoListApiModel>> GetPagerListByHospitalAsync(PagerQuery<PurchaseGoodsBillnoListQueryModel> query, int hospitalDepartmentId);
        Task<PagerResult<PurchaseGoodsBillnoListApiModel>> GetPagerListByClientAsync(PagerQuery<PurchaseGoodsBillnoListQueryModel> query, int clientId);
        PurchaseGoodsBillno Create(PurchaseGoodsBillnoCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, PurchaseGoodsBillnoUpdateApiModel updated);
        int UpdateStatus(int id, BillStatus status);
        Task<PurchaseGoodsBillnoListApiModel> GetIndexAsync(int id);
        Task<IList<PurchaseGoodsBillnoListApiModel>> GetListByPurchaseGoodsIdAsync(int purchaseGoodsId);
    }
}
