using domain.purchase.valuemodel;
using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;

namespace iservice.purchase
{
    public interface IPurchaseGoodsService
    {
        PagerResult<PurchaseGoodsListApiModel> GetPagerList(PagerQuery<PurchaseGoodsListQueryModel> query);
        PurchaseGoods Create(PurchaseGoodsCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, PurchaseGoodsUpdateApiModel updated);
        PagerResult<PurchaseGoodsMappingListApiModel> GetPagerMappingList(PagerQuery<PurchaseGoodsListQueryModel> query);
    }
}
