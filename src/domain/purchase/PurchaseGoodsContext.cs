using domain.client;
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
        public PurchaseGoodsContext(IPurchaseGoodsRespository purchaseGoodsRespositoryy,
            ClientMappingGoodsContext clientMappingGoodsContext,
            StoreContext storeContext)
        {
            _PurchaseGoodsRespository = purchaseGoodsRespositoryy;
            _clientMappingGoodsContext = clientMappingGoodsContext;
            _storeContext = storeContext;
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
            switch (threshold.ThresholdTypeId)
            {
                case (int)PurchaseSettingThresholdType.ByQty:
                    GenerateByQty(purchaseId, threshold, departmentId, userId);
                    break;
                case (int)PurchaseSettingThresholdType.ByPercent:
                    break;
            }
        }

        private void GenerateByQty(int purchaseId, PurchaseSettingThreshold threshold, int departmentId, int userId)
        {
            var store = _storeContext.GetIndexByGoods(departmentId, threshold.HospitalGoodsId);
            if (store.Qty < threshold.DownQty)
            {
                Create(new PurchaseGoodsCreateApiModel
                {
                    HospitalClientId = 0,
                    HospitalGoodsId = threshold.HospitalGoodsId,
                    Qty = threshold.UpQty - store.Qty,
                    PurchaseId = purchaseId,
                }, userId);
            }
        }

    }
}
