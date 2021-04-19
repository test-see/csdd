using domain.store;
using domain.store.enums;
using foundation.config;
using foundation.ef5.poco;
using irespository.purchase;
using irespository.purchase.model;
using irespository.purchase.profile.enums;
using irespository.store.model;
using irespository.store.profile.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace domain.purchase
{
    public class PurchaseGoodsBillnoContext
    {
        private readonly IPurchaseGoodsBillnoRespository _purchaseGoodsBillnoRespository;
        private readonly StoreContext _storeContext;
        private readonly StoreRecordBillnoContext _storeRecordBillnoContext;
        public PurchaseGoodsBillnoContext(IPurchaseGoodsBillnoRespository purchaseGoodsBillnoRespositoryy,
            StoreContext storeContext,
            StoreRecordBillnoContext storeRecordBillnoContext)
        {
            _purchaseGoodsBillnoRespository = purchaseGoodsBillnoRespositoryy;
            _storeContext = storeContext;
            _storeRecordBillnoContext = storeRecordBillnoContext;
        }

        public async Task<PagerResult<PurchaseGoodsBillnoListApiModel>> GetPagerListByHospitalAsync(PagerQuery<PurchaseGoodsBillnoListQueryModel> query, int hospitalId)
        {
            return await _purchaseGoodsBillnoRespository.GetPagerListByHospitalAsync(query, hospitalId);
        }
        public async Task<PagerResult<PurchaseGoodsBillnoListApiModel>> GetPagerListByClientAsync(PagerQuery<PurchaseGoodsBillnoListQueryModel> query, int clientId)
        {
            return await _purchaseGoodsBillnoRespository.GetPagerListByClientAsync(query, clientId);
        }
        public async Task<IList<PurchaseGoodsBillnoListApiModel>> GetListByPurchaseGoodsIdAsync(int purchaseGoodsId)
        {
            return await _purchaseGoodsBillnoRespository.GetListByPurchaseGoodsIdAsync(purchaseGoodsId);
        }

        public PurchaseGoodsBillno Create(PurchaseGoodsBillnoCreateApiModel created, int userId)
        {
            return _purchaseGoodsBillnoRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _purchaseGoodsBillnoRespository.Delete(id);
        }
        public int Update(int id, PurchaseGoodsBillnoUpdateApiModel updated)
        {
            return _purchaseGoodsBillnoRespository.Update(id, updated);
        }

        public async Task<int> ComfirmAsync(IList<int> ids, int userId)
        {
            foreach (var id in ids)
            {
                var goods = await _purchaseGoodsBillnoRespository.GetIndexAsync(id);
                var changed = new StoreChangeApiModel
                {
                    ChangeTypeId = (int)StoreChangeType.Purchase,
                    HospitalChangeGoods = new StoreChangeGoodsValueModel
                    {
                        HospitalGoodId = goods.HospitalGoods.Id,
                        ChangeQty = goods.Qty,
                        Recrdno = RecordNumber.Next((int)StoreChangeType.Purchase, id),
                    },
                };
                var recordId = _storeContext.CreateOrUpdate(changed, goods.Purchase.HospitalDepartment.Id, userId);
                _storeRecordBillnoContext.Create(id, recordId);
                _purchaseGoodsBillnoRespository.UpdateStatus(id, BillStatus.Comfirmed);
            }
            return ids.Count;
        }
        public int Submit(IList<int> ids)
        {
            foreach (var id in ids)
            {
                _purchaseGoodsBillnoRespository.UpdateStatus(id, BillStatus.Submited);
            }
            return ids.Count;
        }
    }
}
