using foundation.config;
using foundation.ef5.poco;
using irespository.purchase;
using irespository.purchase.model;

namespace domain.purchase
{
    public class PurchaseSettingThresholdContext
    {
        private readonly IPurchaseSettingThresholdRespository _purchaseSettingThresholdRespository;
        public PurchaseSettingThresholdContext(IPurchaseSettingThresholdRespository purchaseSettingThresholdRespository)
        {
            _purchaseSettingThresholdRespository = purchaseSettingThresholdRespository;
        }

        public PagerResult<PurchaseSettingThresholdListApiModel> GetPagerList(PagerQuery<PurchaseSettingThresholdListQueryModel> query)
        {
            return _purchaseSettingThresholdRespository.GetPagerList(query);
        }
        public PurchaseSettingThreshold Create(PurchaseSettingThresholdCreateApiModel created, int userId)
        {
            return _purchaseSettingThresholdRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _purchaseSettingThresholdRespository.Delete(id);
        }
        public int Update(int id, PurchaseSettingThresholdUpdateApiModel updated)
        {
            return _purchaseSettingThresholdRespository.Update(id, updated);
        }
    }
}
