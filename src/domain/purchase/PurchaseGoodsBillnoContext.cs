using foundation.config;
using foundation.ef5.poco;
using irespository.purchase;
using irespository.purchase.model;

namespace domain.purchase
{
    public class PurchaseGoodsBillnoContext
    {
        private readonly IPurchaseGoodsBillnoRespository _PurchaseGoodsBillnoRespository;
        public PurchaseGoodsBillnoContext(IPurchaseGoodsBillnoRespository purchaseGoodsBillnoRespositoryy)
        {
            _PurchaseGoodsBillnoRespository = purchaseGoodsBillnoRespositoryy;
        }

        public PagerResult<PurchaseGoodsBillnoListApiModel> GetPagerList(PagerQuery<PurchaseGoodsBillnoListQueryModel> query)
        {
            return _PurchaseGoodsBillnoRespository.GetPagerList(query);
        }


        public PurchaseGoodsBillno Create(PurchaseGoodsBillnoCreateApiModel created, int userId)
        {
            return _PurchaseGoodsBillnoRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _PurchaseGoodsBillnoRespository.Delete(id);
        }
        public int Update(int id, PurchaseGoodsBillnoUpdateApiModel updated)
        {
            return _PurchaseGoodsBillnoRespository.Update(id, updated);
        }
    }
}
