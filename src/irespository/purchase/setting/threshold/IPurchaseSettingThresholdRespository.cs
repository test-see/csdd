using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;
using System.Collections.Generic;

namespace irespository.purchase
{
    public interface IPurchaseSettingThresholdRespository
    {
        PagerResult<PurchaseSettingThresholdListApiModel> GetPagerList(PagerQuery<PurchaseSettingThresholdListQueryModel> query);
        PurchaseSettingThreshold Create(PurchaseSettingThresholdCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, PurchaseSettingThresholdUpdateApiModel updated);
        IList<PurchaseSettingThreshold> GetListBySettingId(int settingId);
    }
}
