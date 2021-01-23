using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;

namespace iservice.purchase
{
    public interface IPurchaseSettingService
    {
        PagerResult<PurchaseSettingListApiModel> GetPagerList(PagerQuery<PurchaseSettingListQueryModel> query);
        PurchaseSetting Create(PurchaseSettingCreateApiModel created, int departmentId, int userId);
        int Delete(int id);
        int Update(int id, PurchaseSettingUpdateApiModel updated);
    }
}
