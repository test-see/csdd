using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;
using irespository.purchase.profile.enums;

namespace irespository.purchase
{
    public interface IPurchaseGoodsRespository
    {
        PagerResult<PurchaseGoodsListApiModel> GetPagerList(PagerQuery<PurchaseGoodsListQueryModel> query);
        PagerResult<PurchaseGoodsListApiModel> GetPagerListByClient(PagerQuery<PurchaseGoodsListQueryModel> query, int clientId);
        PurchaseGoods Create(PurchaseGoodsCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, PurchaseGoodsUpdateApiModel updated);
        PurchaseGoodsListApiModel GetIndex(int id);
        int UpdateStatus(int id, PurchaseGoodsStatus status);
    }
}
