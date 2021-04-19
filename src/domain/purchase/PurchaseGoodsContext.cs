using domain.client;
using domain.hospital;
using domain.store;
using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital.department.model;
using irespository.hospital.goods.model;
using irespository.hospital.model;
using irespository.purchase;
using irespository.purchase.model;
using irespository.purchase.profile.enums;
using irespository.purchase.setting.threshold.enums;
using Mediator.Net;
using storage.hospital.department.carrier;
using System.Linq;
using System.Threading.Tasks;

namespace domain.purchase
{
    public class PurchaseGoodsContext
    {
        private readonly IPurchaseGoodsRespository _purchaseGoodsRespository;
        private readonly StoreContext _storeContext;
        private readonly HospitalGoodsClientService _hospitalGoodsClientContext;
        private readonly StoreRecordContext _storeRecordContext;
        private readonly HospitalDepartmentService _hospitalDepartmentContext;
        private readonly DefaultDbTransaction _defaultDbTransaction;
        private readonly PurchaseGoodsBillnoContext _purchaseGoodsBillnoContext;
        private readonly IMediator _mediator;
        public PurchaseGoodsContext(IPurchaseGoodsRespository purchaseGoodsRespositoryy,
            StoreContext storeContext,
            HospitalGoodsClientService hospitalGoodsClientContext,
            DefaultDbTransaction defaultDbTransaction,
            StoreRecordContext storeRecordContext,
            HospitalDepartmentService hospitalDepartmentContext,
            PurchaseGoodsBillnoContext purchaseGoodsBillnoContext,
            IMediator mediator)
        {
            _purchaseGoodsRespository = purchaseGoodsRespositoryy;
            _storeContext = storeContext;
            _hospitalGoodsClientContext = hospitalGoodsClientContext;
            _storeRecordContext = storeRecordContext;
            _hospitalDepartmentContext = hospitalDepartmentContext;
            _defaultDbTransaction = defaultDbTransaction;
            _purchaseGoodsBillnoContext = purchaseGoodsBillnoContext;
            _mediator = mediator;
        }

        public async Task<PagerResult<PurchaseGoodsListApiModel>> GetPagerListAsync(PagerQuery<PurchaseGoodsListQueryModel> query)
        {
            return await _purchaseGoodsRespository.GetPagerListAsync(query);
        }

        public async Task<PagerResult<PurchaseGoodsListApiModel>> GetPagerListByClientAsync(PagerQuery<PurchaseGoodsListQueryModel> query, int clientId)
        {
            return await _purchaseGoodsRespository.GetPagerListByClientAsync(query, clientId);          
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

        public async Task GenerateAsync(int purchaseId, PurchaseSettingThreshold threshold, int departmentId, int userId)
        {
            var clients = await _mediator.ListAsync<ListHospitalGoodsClientRequest, ListHospitalGoodsClientResponse>(new ListHospitalGoodsClientRequest
            {
                HospitalGoodsId = threshold.HospitalGoodsId
            });
            if (!clients.Any()) return;

            var qty = await GetPurchaseGoodsQtyAsync(threshold, departmentId);
            if (qty <= 0) return;

            Create(new PurchaseGoodsCreateApiModel
            {
                HospitalClientId = clients.First().HospitalClient.Id,
                HospitalGoodsId = threshold.HospitalGoodsId,
                Qty = qty,
                PurchaseId = purchaseId,
            }, userId);
        }

        private async Task<int> GetPurchaseGoodsQtyAsync(PurchaseSettingThreshold threshold, int departmentId)
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
                    qty = await GetPurchaseGoodsQtyByPercentAsync(threshold, storeQty, departmentId);
                    break;
            }
            return qty;
        }

        private async Task<int> GetPurchaseGoodsQtyByPercentAsync(PurchaseSettingThreshold threshold, int storeQty, int departmentId)
        {
            var department = await _mediator.GetByIdAsync<GetHospitalDepartmentRequest, GetHospitalDepartmentResponse>(departmentId);
            var total = _storeRecordContext.GetConsumeAmount(departmentId, threshold.HospitalGoodsId, department.Hospital.ConsumeDays);
            var average = total / department.Hospital.ConsumeDays;
            if (storeQty < average * threshold.DownQty)
                return average * threshold.UpQty - storeQty;
            return 0;
        }
        
        public async Task<PurchaseGoodsListApiModel> GetIndexAsync(int id)
        {
            return await _purchaseGoodsRespository.GetIndexAsync(id);
        }

        public async Task<int> SubmitAsync(int id)
        {
            using (var trans = _defaultDbTransaction.Begin())
            {
                var bills = await _purchaseGoodsBillnoContext.GetListByPurchaseGoodsIdAsync(id);
                _purchaseGoodsBillnoContext.Submit(bills.Select(x => x.Id).ToList());
                var result = _purchaseGoodsRespository.UpdateStatus(id, PurchaseGoodsStatus.Submited);
                trans.Commit();
                return result;
            }
        }
        
    }
}
