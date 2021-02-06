using foundation.config;
using foundation.ef5.poco;
using irespository.purchase;
using irespository.purchase.model;

namespace domain.purchase
{
    public class PurchaseGoodsContext
    {
        private readonly IPurchaseGoodsRespository _PurchaseGoodsRespository;
        public PurchaseGoodsContext(IPurchaseGoodsRespository purchaseGoodsRespositoryy)
        {
            _PurchaseGoodsRespository = purchaseGoodsRespositoryy;
        }

        public PagerResult<PurchaseGoodsListApiModel> GetPagerList(PagerQuery<PurchaseGoodsListQueryModel> query)
        {
            return _PurchaseGoodsRespository.GetPagerList(query);
        }
        public PurchaseGoods Create(PurchaseGoodsCreateApiModel created, int userId)
        {
            return _PurchaseGoodsRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _PurchaseGoodsRespository.Delete(id);
        }
        public int Update(int id, PurchaseGoodsUpdateApiModel updated)
        {
            return _PurchaseGoodsRespository.Update(id, updated);
        }
    }
}
