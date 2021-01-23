using foundation.config;
using foundation.ef5.poco;
using irespository.purchase;
using irespository.purchase.model;

namespace domain.purchase
{
    public class PurchaseSettingContext
    {
        private readonly IPurchaseSettingRespository _purchaseSettingRespository;
        public PurchaseSettingContext(IPurchaseSettingRespository purchaseSettingRespository)
        {
            _purchaseSettingRespository = purchaseSettingRespository;
        }

        public PagerResult<PurchaseSettingListApiModel> GetPagerList(PagerQuery<PurchaseSettingListQueryModel> query)
        {
            return _purchaseSettingRespository.GetPagerList(query);
        }
        public PurchaseSetting Create(PurchaseSettingCreateApiModel created, int departmentId, int userId)
        {
            return _purchaseSettingRespository.Create(created, departmentId, userId);
        }
        public int Delete(int id)
        {
            return _purchaseSettingRespository.Delete(id);
        }
        public int Update(int id, PurchaseSettingUpdateApiModel updated)
        {
            return _purchaseSettingRespository.Update(id, updated);
        }
    }
}
