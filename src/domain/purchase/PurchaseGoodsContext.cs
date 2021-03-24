using domain.client;
using domain.hospital;
using domain.store;
using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.purchase;
using irespository.purchase.model;
using irespository.purchase.profile.enums;
using irespository.purchase.setting.threshold.enums;
using System.Linq;

namespace domain.purchase
{
    public class PurchaseGoodsContext
    {
        private readonly IPurchaseGoodsRespository _purchaseGoodsRespository;
        private readonly StoreContext _storeContext;
        private readonly HospitalGoodsClientContext _hospitalGoodsClientContext;
        private readonly StoreRecordContext _storeRecordContext;
        private readonly HospitalDepartmentContext _hospitalDepartmentContext;
        private readonly DefaultDbTransaction _defaultDbTransaction;
        private readonly PurchaseGoodsBillnoContext _purchaseGoodsBillnoContext;
        public PurchaseGoodsContext(IPurchaseGoodsRespository purchaseGoodsRespositoryy,
            StoreContext storeContext,
            HospitalGoodsClientContext hospitalGoodsClientContext,
            DefaultDbTransaction defaultDbTransaction,
            StoreRecordContext storeRecordContext,
            HospitalDepartmentContext hospitalDepartmentContext,
            PurchaseGoodsBillnoContext purchaseGoodsBillnoContext)
        {
            _purchaseGoodsRespository = purchaseGoodsRespositoryy;
            _storeContext = storeContext;
            _hospitalGoodsClientContext = hospitalGoodsClientContext;
            _storeRecordContext = storeRecordContext;
            _hospitalDepartmentContext = hospitalDepartmentContext;
            _defaultDbTransaction = defaultDbTransaction;
            _purchaseGoodsBillnoContext = purchaseGoodsBillnoContext;
        }

        public PagerResult<PurchaseGoodsListApiModel> GetPagerList(PagerQuery<PurchaseGoodsListQueryModel> query)
        {
            return _purchaseGoodsRespository.GetPagerList(query);
        }

        public PagerResult<PurchaseGoodsListApiModel> GetPagerListByClient(PagerQuery<PurchaseGoodsListQueryModel> query, int clientId)
        {
            return _purchaseGoodsRespository.GetPagerListByClient(query, clientId);          
        }

        public PurchaseGoods Create(PurchaseGoodsCreateApiModel created, int userId)
        {
            return _purchaseGoodsRespository.Create(created, userId);
        }
        
        public int Delete(int id)
        {
            return _purchaseGoodsRespository.Delete(id);
        }
        
        public int Update(int id, PurchaseGoodsUpdateApiModel updated)
        {
            return _purchaseGoodsRespository.Update(id, updated);
        }

        public void Generate(int purchaseId, PurchaseSettingThreshold threshold, int departmentId, int userId)
        {
            var clients = _hospitalGoodsClientContext.GetListByGoodsId(threshold.HospitalGoodsId);
            if (!clients.Any()) return;

            var qty = GetPurchaseGoodsQty(threshold, departmentId);
            if (qty <= 0) return;

            Create(new PurchaseGoodsCreateApiModel
            {
                HospitalClientId = clients.First().HospitalClient.Id,
                HospitalGoodsId = threshold.HospitalGoodsId,
                Qty = qty,
                PurchaseId = purchaseId,
            }, userId);
        }

        private int GetPurchaseGoodsQty(PurchaseSettingThreshold threshold, int departmentId)
        {
            var store = _storeContext.GetIndexByGoods(departmentId, threshold.HospitalGoodsId);
            var storeQty = store?.Qty ?? 0;
            var qty = 0;
            switch (threshold.ThresholdTypeId)
            {
                case (int)PurchaseSettingThresholdType.ByQty:
                    if (storeQty < threshold.DownQty)
                        qty = threshold.UpQty - storeQty;
                    break;
                case (int)PurchaseSettingThresholdType.ByPercent:
                    qty = GetPurchaseGoodsQtyByPercent(threshold, storeQty, departmentId);
                    break;
            }
            return qty;
        }

        private int GetPurchaseGoodsQtyByPercent(PurchaseSettingThreshold threshold, int storeQty, int departmentId)
        {
            var department = _hospitalDepartmentContext.GetValue(departmentId);
            var total = _storeRecordContext.GetConsumeAmount(departmentId, threshold.HospitalGoodsId, department.Hospital.ConsumeDays);
            var average = total / department.Hospital.ConsumeDays;
            if (storeQty < average * threshold.DownQty)
                return average * threshold.UpQty - storeQty;
            return 0;
        }
        
        public PurchaseGoodsListApiModel GetIndex(int id)
        {
            return _purchaseGoodsRespository.GetIndex(id);
        }

        public int Submit(int id)
        {
            using (var trans = _defaultDbTransaction.Begin())
            {
                var bills = _purchaseGoodsBillnoContext.GetListByPurchaseGoodsId(id);
                _purchaseGoodsBillnoContext.Submit(bills.Select(x => x.Id).ToList());
                return _purchaseGoodsRespository.UpdateStatus(id, PurchaseGoodsStatus.Submited);
            }
        }
        
    }
}
