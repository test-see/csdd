using domain.purchase;
using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;
using iservice.purchase;
using System.Threading.Tasks;

namespace service.purchase
{
    public class PurchaseSettingService : IPurchaseSettingService
    {
        private readonly PurchaseSettingContext _purchaseSettingContext;
        public PurchaseSettingService(PurchaseSettingContext purchaseSettingContext)
        {
            _purchaseSettingContext = purchaseSettingContext;
        }
        public async Task<PagerResult<PurchaseSettingListApiModel>> GetPagerListAsync(PagerQuery<PurchaseSettingListQueryModel> query, int hospitalId)
        {
            return await _purchaseSettingContext.GetPagerListAsync(query, hospitalId);
        }
        public PurchaseSetting Create(PurchaseSettingCreateApiModel created, int departmentId, int userId)
        {
            return _purchaseSettingContext.Create(created, departmentId, userId);
        }

        public int Delete(int id)
        {
            return _purchaseSettingContext.Delete(id);
        }

        public int Update(int id, PurchaseSettingUpdateApiModel updated)
        {
            return _purchaseSettingContext.Update(id, updated);
        }

        public async Task<PurchaseSettingIndexApiModel> GetIndexAsync(int id)
        {
            return await _purchaseSettingContext.GetIndexAsync(id);
        }
    }
}
