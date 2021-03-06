using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;
using irespository.purchase.profile.enums;

namespace irespository.purchase
{
    public interface IPurchaseRespository
    {
        PagerResult<PurchaseListApiModel> GetPagerList(PagerQuery<PurchaseListQueryModel> query, int hospitalId);
        Purchase Create(PurchaseCreateApiModel created, int departmentId, int userId);
        int Delete(int id);
        int Update(int id, PurchaseUpdateApiModel updated);
        PurchaseIndexApiModel GetIndex(int id);
        int UpdateStatus(int id, PurchaseStatus status);
    }
}
