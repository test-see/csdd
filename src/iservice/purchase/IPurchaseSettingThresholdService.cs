using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;
using System.Collections.Generic;

namespace iservice.purchase
{
    public interface IPurchaseSettingThresholdService
    {
        PagerResult<PurchaseSettingThresholdListApiModel> GetPagerList(PagerQuery<PurchaseSettingThresholdListQueryModel> query, int hospitalId);
        PurchaseSettingThreshold Create(PurchaseSettingThresholdCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, PurchaseSettingThresholdUpdateApiModel updated);
        IEnumerable<DataPurchaseThresholdType> GetThresholdTypeList();
    }
}
