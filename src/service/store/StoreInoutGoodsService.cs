using domain.storeinout;
using foundation.config;
using foundation.ef5.poco;
using irespository.storeinout.model;
using iservice.store;
using System.Threading.Tasks;

namespace service.store
{
    public class StoreInoutGoodsService : IStoreInoutGoodsService
    {
        private readonly StoreInoutGoodsContext _StoreInoutGoodsContext;
        public StoreInoutGoodsService(StoreInoutGoodsContext StoreInoutGoodsContext)
        {
            _StoreInoutGoodsContext = StoreInoutGoodsContext;
        }
        public async Task<PagerResult<StoreInoutGoodsListApiModel>> GetPagerListAsync(PagerQuery<StoreInoutGoodsListQueryModel> query)
        {
            return await _StoreInoutGoodsContext.GetPagerListAsync(query);
        }
        public StoreInoutGoods Create(StoreInoutGoodsCreateApiModel created, int userId)
        {
            return _StoreInoutGoodsContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _StoreInoutGoodsContext.Delete(id);
        }

        public int Update(int id, StoreInoutGoodsUpdateApiModel updated)
        {
            return _StoreInoutGoodsContext.Update(id, updated);
        }
    }
}
