using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;

namespace iservice.purchase
{
    public interface IPurchaseSettingThresholdService
    {
        PagerResult<PurchaseSettingThresholdListApiModel> GetPagerList(PagerQuery<PurchaseSettingThresholdListQueryModel> query);
        PurchaseSettingThreshold Create(PurchaseSettingThresholdCreateApiModel created, int departmentId, int userId);
        int Delete(int id);
        int Update(int id, PurchaseSettingThresholdUpdateApiModel updated);
    }
}
