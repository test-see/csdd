using domain.purchase;
using domain.purchase.valuemodel;
using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;
using iservice.purchase;

namespace service.purchase
{
    public class PurchaseGoodsService : IPurchaseGoodsService
    {
        private readonly PurchaseGoodsContext _PurchaseGoodsContext;
        public PurchaseGoodsService(PurchaseGoodsContext PurchaseGoodsContext)
        {
            _PurchaseGoodsContext = PurchaseGoodsContext;
        }
        public PagerResult<PurchaseGoodsListApiModel> GetPagerList(PagerQuery<PurchaseGoodsListQueryModel> query)
        {
            return _PurchaseGoodsContext.GetPagerList(query);
        }

        public PurchaseGoods Create(PurchaseGoodsCreateApiModel created, int userId)
        {
            return _PurchaseGoodsContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _PurchaseGoodsContext.Delete(id);
        }

        public int Update(int id, PurchaseGoodsUpdateApiModel updated)
        {
            return _PurchaseGoodsContext.Update(id, updated);
        }

        public PagerResult<PurchaseGoodsMappingListApiModel> GetPagerMappingList(PagerQuery<PurchaseGoodsListQueryModel> query, int clientId)
        {
            return _PurchaseGoodsContext.GetPagerMappingList(query, clientId);
        }
    }
}
