using domain.purchase;
using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;
using iservice.purchase;
using System.Threading.Tasks;

namespace service.purchase
{
    public class PurchaseGoodsService : IPurchaseGoodsService
    {
        private readonly PurchaseGoodsContext _purchaseGoodsContext;
        public PurchaseGoodsService(PurchaseGoodsContext purchaseGoodsContext)
        {
            _purchaseGoodsContext = purchaseGoodsContext;
        }
        public async Task<PagerResult<PurchaseGoodsListApiModel>> GetPagerListAsync(PagerQuery<PurchaseGoodsListQueryModel> query)
        {
            return await _purchaseGoodsContext.GetPagerListAsync(query);
        }

        public PurchaseGoods Create(PurchaseGoodsCreateApiModel created, int userId)
        {
            return _purchaseGoodsContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _purchaseGoodsContext.Delete(id);
        }

        public int Update(int id, PurchaseGoodsUpdateApiModel updated)
        {
            return _purchaseGoodsContext.Update(id, updated);
        }

        public async Task<PagerResult<PurchaseGoodsListApiModel>> GetPagerListByClientAsync(PagerQuery<PurchaseGoodsListQueryModel> query, int clientId)
        {
            return await _purchaseGoodsContext.GetPagerListByClientAsync(query, clientId);
        }
        public async Task<PurchaseGoodsListApiModel> GetIndexAsync(int id)
        {
            return await _purchaseGoodsContext.GetIndexAsync(id);
        }
        public async Task<int> SubmitAsync(int id)
        {
            return await _purchaseGoodsContext.SubmitAsync(id);
        }
    }
}
