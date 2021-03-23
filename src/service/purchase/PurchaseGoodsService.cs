using domain.purchase;
using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;
using iservice.purchase;

namespace service.purchase
{
    public class PurchaseGoodsService : IPurchaseGoodsService
    {
        private readonly PurchaseGoodsContext _purchaseGoodsContext;
        public PurchaseGoodsService(PurchaseGoodsContext purchaseGoodsContext)
        {
            _purchaseGoodsContext = purchaseGoodsContext;
        }
        public PagerResult<PurchaseGoodsListApiModel> GetPagerList(PagerQuery<PurchaseGoodsListQueryModel> query)
        {
            return _purchaseGoodsContext.GetPagerList(query);
        }

        public PurchaseGoods Create(PurchaseGoodsCreateApiModel created, int userId)
        {
            return _purchaseGoodsContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _purchaseGoodsContext.Delete(id);
        }

        public int Update(int id, PurchaseGoodsUpdateApiModel updated)
        {
            return _purchaseGoodsContext.Update(id, updated);
        }

        public PagerResult<PurchaseGoodsListApiModel> GetPagerListByClient(PagerQuery<PurchaseGoodsListQueryModel> query, int clientId)
        {
            return _purchaseGoodsContext.GetPagerListByClient(query, clientId);
        }
        public PurchaseGoodsListApiModel GetIndex(int id)
        {
            return _purchaseGoodsContext.GetIndex(id);
        }
        public int Submit(int id)
        {
            return _purchaseGoodsContext.Submit(id);
        }
    }
}
