using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;
using System.Threading.Tasks;

namespace irespository.purchase
{
    public interface IPurchaseSettingRespository
    {
        Task<PagerResult<PurchaseSettingListApiModel>> GetPagerListAsync(PagerQuery<PurchaseSettingListQueryModel> query, int hospitalId);
        PurchaseSetting Create(PurchaseSettingCreateApiModel created, int departmentId, int userId);
        int Delete(int id);
        int Update(int id, PurchaseSettingUpdateApiModel updated);
        Task<PurchaseSettingIndexApiModel> GetIndexAsync(int id);
    }
}
