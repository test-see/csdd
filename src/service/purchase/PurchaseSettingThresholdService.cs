using domain.purchase;
using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;
using iservice.purchase;
using System.Collections.Generic;

namespace service.purchase
{
    public class PurchaseSettingThresholdService : IPurchaseSettingThresholdService
    {
        private readonly PurchaseSettingThresholdContext _purchaseSettingThresholdContext;
        public PurchaseSettingThresholdService(PurchaseSettingThresholdContext purchaseSettingThresholdContext)
        {
            _purchaseSettingThresholdContext = purchaseSettingThresholdContext;
        }
        public PagerResult<PurchaseSettingThresholdListApiModel> GetPagerList(PagerQuery<PurchaseSettingThresholdListQueryModel> query, int hospitalId)
        {
            return _purchaseSettingThresholdContext.GetPagerList(query, hospitalId);
        }
        public PurchaseSettingThreshold Create(PurchaseSettingThresholdCreateApiModel created, int userId)
        {
            return _purchaseSettingThresholdContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _purchaseSettingThresholdContext.Delete(id);
        }

        public int Update(int id, PurchaseSettingThresholdUpdateApiModel updated)
        {
            return _purchaseSettingThresholdContext.Update(id, updated);
        }

        public IEnumerable<DataPurchaseThresholdType> GetThresholdTypeList()
        {
            return _purchaseSettingThresholdContext.GetThresholdTypeList();
        }
    }
}
