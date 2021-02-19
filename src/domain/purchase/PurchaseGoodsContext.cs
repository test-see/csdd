using domain.client;
using domain.purchase.valuemodel;
using foundation.config;
using foundation.ef5.poco;
using irespository.purchase;
using irespository.purchase.model;
using System.Linq;

namespace domain.purchase
{
    public class PurchaseGoodsContext
    {
        private readonly IPurchaseGoodsRespository _PurchaseGoodsRespository;
        private readonly ClientMappingGoodsContext _clientMappingGoodsContext;
        public PurchaseGoodsContext(IPurchaseGoodsRespository purchaseGoodsRespositoryy,
            ClientMappingGoodsContext clientMappingGoodsContext)
        {
            _PurchaseGoodsRespository = purchaseGoodsRespositoryy;
            _clientMappingGoodsContext = clientMappingGoodsContext;
        }

        public PagerResult<PurchaseGoodsListApiModel> GetPagerList(PagerQuery<PurchaseGoodsListQueryModel> query)
        {
            return _PurchaseGoodsRespository.GetPagerList(query);
        }

        public PagerResult<PurchaseGoodsMappingListApiModel> GetPagerMappingList(PagerQuery<PurchaseGoodsListQueryModel> query, int clientId)
        {
            query.Query = query.Query ?? new PurchaseGoodsListQueryModel { };
            query.Query.ClientId = clientId;
            var data = GetPagerList(query);
            var result = data.Result.Select(x => new PurchaseGoodsMappingListApiModel
            {
                PurchaseGoods = x,
                ClientMappingGoods = _clientMappingGoodsContext.GetIndexByHospitalGoodsId(x.HospitalGoods.Id, clientId),
            });
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
    }
}
