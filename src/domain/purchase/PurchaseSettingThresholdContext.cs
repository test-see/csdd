using foundation.config;
using foundation.ef5.poco;
using irespository.data;
using irespository.purchase;
using irespository.purchase.model;
using System.Collections.Generic;

namespace domain.purchase
{
    public class PurchaseSettingThresholdContext
    {
        private readonly IPurchaseSettingThresholdRespository _purchaseSettingThresholdRespository;
        private readonly IPurchaseThresholdTypeRespository _purchaseThresholdTypeRespository;
        public PurchaseSettingThresholdContext(IPurchaseSettingThresholdRespository purchaseSettingThresholdRespository,
            IPurchaseThresholdTypeRespository purchaseThresholdTypeRespository)
        {
            _purchaseSettingThresholdRespository = purchaseSettingThresholdRespository;
            _purchaseThresholdTypeRespository = purchaseThresholdTypeRespository;
        }

        public PagerResult<PurchaseSettingThresholdListApiModel> GetPagerList(PagerQuery<PurchaseSettingThresholdListQueryModel> query, int hospitalId)
        {
            return _purchaseSettingThresholdRespository.GetPagerList(query, hospitalId);
        }
        public PurchaseSettingThreshold Create(PurchaseSettingThresholdCreateApiModel created, int userId)
        {
            return _purchaseSettingThresholdRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _purchaseSettingThresholdRespository.Delete(id);
        }
        public int Update(int id, PurchaseSettingThresholdUpdateApiModel updated)
        {
            return _purchaseSettingThresholdRespository.Update(id, updated);
        }
        public IEnumerable<DataPurchaseThresholdType> GetThresholdTypeList()
        {
            return _purchaseThresholdTypeRespository.GetList();
        }
        public IList<PurchaseSettingThreshold> GetListBySettingId(int settingId)
        {
            return _purchaseSettingThresholdRespository.GetListBySettingId(settingId);
        }
    }
}
