using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;
using System.Threading.Tasks;

namespace iservice.purchase
{
    public interface IPurchaseService
    {
        PagerResult<PurchaseListApiModel> GetPagerList(PagerQuery<PurchaseListQueryModel> query);
        Task<Purchase> CreateAsync(PurchaseCreateApiModel created, int departmentId, int userId);
        int Delete(int id);
        int Update(int id, PurchaseUpdateApiModel updated);
        PurchaseIndexApiModel GetIndex(int id);
        int Submit(int id);
        int Comfirm(int id);
        int Revoke(int id);
        Task GenerateAsync(int id);
    }
}
