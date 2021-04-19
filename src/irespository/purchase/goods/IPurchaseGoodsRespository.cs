using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;
using irespository.purchase.profile.enums;
using System.Threading.Tasks;

namespace irespository.purchase
{
    public interface IPurchaseGoodsRespository
    {
        Task<PagerResult<PurchaseGoodsListApiModel>> GetPagerListAsync(PagerQuery<PurchaseGoodsListQueryModel> query);
        Task<PagerResult<PurchaseGoodsListApiModel>> GetPagerListByClientAsync(PagerQuery<PurchaseGoodsListQueryModel> query, int clientId);
        PurchaseGoods Create(PurchaseGoodsCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, PurchaseGoodsUpdateApiModel updated);
        Task<PurchaseGoodsListApiModel> GetIndexAsync(int id);
        int UpdateStatus(int id, PurchaseGoodsStatus status);
    }
}
