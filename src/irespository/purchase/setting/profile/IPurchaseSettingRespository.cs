using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;

namespace irespository.purchase
{
    public interface IPurchaseSettingRespository
    {
        PagerResult<PurchaseSettingListApiModel> GetPagerList(PagerQuery<PurchaseSettingListQueryModel> query, int hospitalId);
        PurchaseSetting Create(PurchaseSettingCreateApiModel created, int departmentId, int userId);
        int Delete(int id);
        int Update(int id, PurchaseSettingUpdateApiModel updated);
        PurchaseSettingIndexApiModel GetIndex(int id);
    }
}
