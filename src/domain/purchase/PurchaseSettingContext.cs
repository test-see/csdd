using foundation.config;
using foundation.ef5.poco;
using irespository.purchase;
using irespository.purchase.model;
using System.Threading.Tasks;

namespace domain.purchase
{
    public class PurchaseSettingContext
    {
        private readonly IPurchaseSettingRespository _purchaseSettingRespository;
        public PurchaseSettingContext(IPurchaseSettingRespository purchaseSettingRespository)
        {
            _purchaseSettingRespository = purchaseSettingRespository;
        }

        public async Task<PagerResult<PurchaseSettingListApiModel>> GetPagerListAsync(PagerQuery<PurchaseSettingListQueryModel> query, int hospitalId)
        {
            return await _purchaseSettingRespository.GetPagerListAsync(query, hospitalId);
        }
        public PurchaseSetting Create(PurchaseSettingCreateApiModel created, int departmentId, int userId)
        {
            return _purchaseSettingRespository.Create(created, departmentId, userId);
        }
        public int Delete(int id)
        {
            return _purchaseSettingRespository.Delete(id);
        }
        public int Update(int id, PurchaseSettingUpdateApiModel updated)
        {
            return _purchaseSettingRespository.Update(id, updated);
        }
        public async Task<PurchaseSettingIndexApiModel> GetIndexAsync(int id)
        {
            var goods = await _purchaseSettingRespository.GetIndexAsync(id);
            return goods;
        }
    }
}
