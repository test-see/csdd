using domain.client;
using domain.hospital;
using domain.purchase.valuemodel;
using domain.store;
using foundation.config;
using foundation.ef5.poco;
using irespository.purchase;
using irespository.purchase.goods.model;
using irespository.purchase.model;
using irespository.purchase.setting.threshold.enums;
using System.Collections.Generic;
using System.Linq;

namespace domain.purchase
{
    public class PurchaseGoodsContext
    {
        private readonly IPurchaseGoodsRespository _PurchaseGoodsRespository;
        private readonly ClientMappingGoodsContext _clientMappingGoodsContext;
        private readonly StoreContext _storeContext;
        private readonly HospitalGoodsClientContext _hospitalGoodsClientContext;
        public PurchaseGoodsContext(IPurchaseGoodsRespository purchaseGoodsRespositoryy,
            ClientMappingGoodsContext clientMappingGoodsContext,
            StoreContext storeContext,
            HospitalGoodsClientContext hospitalGoodsClientContext)
        {
            _PurchaseGoodsRespository = purchaseGoodsRespositoryy;
            _clientMappingGoodsContext = clientMappingGoodsContext;
            _storeContext = storeContext;
            _hospitalGoodsClientContext = hospitalGoodsClientContext;
        }

        public PagerResult<PurchaseGoodsListApiModel> GetPagerList(PagerQuery<PurchaseGoodsListQueryModel> query)
        {
            return _PurchaseGoodsRespository.GetPagerList(query);
        }

        public PagerResult<PurchaseGoodsMappingListApiModel> GetPagerMappingList(PagerQuery<PurchaseGoodsListQueryModel> query, int clientId)
        {
            var data = _PurchaseGoodsRespository.GetPagerListByClient(query, clientId);
            var result = new List<PurchaseGoodsMappingListApiModel>();
            foreach (var x in data.Result)
            {
                var mapping = _clientMappingGoodsContext.GetIndexByHospitalGoodsId(x.HospitalGoods.Id, clientId);
                result.Add(new PurchaseGoodsMappingListApiModel
                {
                    PurchaseGoods = x,
                    MappingClientGoods = new MappingClientGoodsValueModel
                    {
                        ClientGoods = mapping.ClientGoods,
                        Qty = x.Qty * mapping.ClientQty / mapping.HospitalQty,
                    },
                });
            }
            return new PagerResult<PurchaseGoodsMappingListApiModel>
            {
                Index = data.Index,
                Size = data.Size,
                Total = data.Total,
                Result = result
            };
        }

        public PurchaseGoods Create(PurchaseGoodsCreateApiModel created, int userId)
        {
            return _PurchaseGoodsRespository.Create(created, userId);
        }
        
        public int Delete(int id)
        {
            return _PurchaseGoodsRespository.Delete(id);
        }
        
        public int Update(int id, PurchaseGoodsUpdateApiModel updated)
        {
            return _PurchaseGoodsRespository.Update(id, updated);
        }

        public void Generate(int purchaseId, PurchaseSettingThreshold threshold, int departmentId, int userId)
        {
            var clients = _hospitalGoodsClientContext.GeListByGoodsId(threshold.HospitalGoodsId);
            if (!clients.Any()) return;
            var qty = 0;
            switch (threshold.ThresholdTypeId)
            {
                case (int)PurchaseSettingThresholdType.ByQty:
                    qty = GenerateByQty(threshold, departmentId);
                    break;
                case (int)PurchaseSettingThresholdType.ByPercent:
                    qty = GenerateByPercent(threshold, departmentId);
                    break;
            }

            if (qty <= 0) return;
            Create(new PurchaseGoodsCreateApiModel
            {
                HospitalClientId = clients.First().HospitalClient.Id,
                HospitalGoodsId = threshold.HospitalGoodsId,
                Qty = qty,
                PurchaseId = purchaseId,
            }, userId);
        }

        private int GenerateByQty(PurchaseSettingThreshold threshold, int departmentId)
        {
            var store = _storeContext.GetIndexByGoods(departmentId, threshold.HospitalGoodsId);
            if (store.Qty < threshold.DownQty)
            {
                return threshold.UpQty - store.Qty;
            }
            return 0;
        }

        private int GenerateByPercent(PurchaseSettingThreshold threshold, int departmentId)
        {

            return 0;
        }
    }
}
