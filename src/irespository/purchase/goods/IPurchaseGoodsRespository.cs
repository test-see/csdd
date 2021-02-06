using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;

namespace irespository.purchase
{
    public interface IPurchaseGoodsRespository
    {
        PagerResult<PurchaseGoodsListApiModel> GetPagerList(PagerQuery<PurchaseGoodsListQueryModel> query);
        PurchaseGoods Create(PurchaseGoodsCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, PurchaseGoodsUpdateApiModel updated);
    }
}
