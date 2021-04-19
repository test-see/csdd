using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;
using System.Threading.Tasks;

namespace iservice.purchase
{
    public interface IPurchaseGoodsService
    {
        Task<PagerResult<PurchaseGoodsListApiModel>> GetPagerListAsync(PagerQuery<PurchaseGoodsListQueryModel> query);
        PurchaseGoods Create(PurchaseGoodsCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, PurchaseGoodsUpdateApiModel updated);
        Task<PagerResult<PurchaseGoodsListApiModel>> GetPagerListByClientAsync(PagerQuery<PurchaseGoodsListQueryModel> query, int clientId);
        Task<PurchaseGoodsListApiModel> GetIndexAsync(int id);
        Task<int> SubmitAsync(int id);
    }
}
