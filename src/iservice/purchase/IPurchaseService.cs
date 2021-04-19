using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;
using System.Threading.Tasks;

namespace iservice.purchase
{
    public interface IPurchaseService
    {
        Task<PagerResult<PurchaseListApiModel>> GetPagerListAsync(PagerQuery<PurchaseListQueryModel> query);
        Task<Purchase> CreateAsync(PurchaseCreateApiModel created, int departmentId, int userId);
        int Delete(int id);
        int Update(int id, PurchaseUpdateApiModel updated);
        Task<PurchaseIndexApiModel> GetIndexAsync(int id);
        int Submit(int id);
        int Comfirm(int id);
        int Revoke(int id);
        Task GenerateAsync(int id);
    }
}
