using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iservice.purchase
{
    public interface IPurchaseSettingThresholdService
    {
        Task<PagerResult<PurchaseSettingThresholdListApiModel>> GetPagerListAsync(PagerQuery<PurchaseSettingThresholdListQueryModel> query);
        PurchaseSettingThreshold Create(PurchaseSettingThresholdCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, PurchaseSettingThresholdUpdateApiModel updated);
        IEnumerable<DataPurchaseThresholdType> GetThresholdTypeList();
    }
}
