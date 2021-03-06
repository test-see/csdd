using foundation.config;
using foundation.ef5.poco;
using irespository.purchase;
using irespository.purchase.model;
using irespository.purchase.profile.enums;

namespace domain.purchase
{
    public class PurchaseContext
    {
        private readonly IPurchaseRespository _purchaseRespository;
        private readonly PurchaseSettingThresholdContext _purchaseSettingThresholdContext;
        private readonly PurchaseGoodsContext _purchaseGoodsContext;
        public PurchaseContext(IPurchaseRespository purchaseRespository,
            PurchaseSettingThresholdContext purchaseSettingThresholdContext,
            PurchaseGoodsContext purchaseGoodsContext)
        {
            _purchaseRespository = purchaseRespository;
            _purchaseSettingThresholdContext = purchaseSettingThresholdContext;
            _purchaseGoodsContext = purchaseGoodsContext;
        }

        public PagerResult<PurchaseListApiModel> GetPagerList(PagerQuery<PurchaseListQueryModel> query, int hospitalId)
        {
            return _purchaseRespository.GetPagerList(query, hospitalId);
        }
        public Purchase Create(PurchaseCreateApiModel created, int departmentId, int userId)
        {
            var purchase = _purchaseRespository.Create(created, departmentId, userId);
            if (created.PurchaseSettingId != null)
            {
                var thresholds = _purchaseSettingThresholdContext.GetListBySettingId(created.PurchaseSettingId.Value);
                foreach(var item in thresholds)
                {
                    _purchaseGoodsContext.Generate(purchase.Id, item, departmentId, userId);
                }
            }
            return purchase;
        }
        public int Delete(int id)
        {
            return _purchaseRespository.Delete(id);
        }
        public int Update(int id, PurchaseUpdateApiModel updated)
        {
            return _purchaseRespository.Update(id, updated);
        }
        public PurchaseIndexApiModel GetIndex(int id)
        {
            var goods = _purchaseRespository.GetIndex(id);
            return goods;
        }
        public int Submit(int id)
        {
            return _purchaseRespository.UpdateStatus(id, PurchaseStatus.Submited);
        }
        public int Comfirm(int id)
        {
            return _purchaseRespository.UpdateStatus(id, PurchaseStatus.Comfirmed);
        }
    }
}
