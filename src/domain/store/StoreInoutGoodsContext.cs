using foundation.config;
using foundation.ef5.poco;
using irespository.storeinout;
using irespository.storeinout.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace domain.storeinout
{
    public class StoreInoutGoodsContext
    {
        private readonly IStoreInoutGoodsRespository _StoreInoutGoodsRespository;
        public StoreInoutGoodsContext(IStoreInoutGoodsRespository StoreInoutGoodsRespositoryy)
        {
            _StoreInoutGoodsRespository = StoreInoutGoodsRespositoryy;
        }

        public async Task<PagerResult<StoreInoutGoodsListApiModel>> GetPagerListAsync(PagerQuery<StoreInoutGoodsListQueryModel> query)
        {
            return await _StoreInoutGoodsRespository.GetPagerListAsync(query);
        }
        public async Task<IList<StoreInoutGoodsListApiModel>> GetListByStoreInoutAsync(int storeInoutId)
        {
            return await _StoreInoutGoodsRespository.GetListByStoreInoutAsync(storeInoutId);
        }
        public StoreInoutGoods Create(StoreInoutGoodsCreateApiModel created, int userId)
        {
            return _StoreInoutGoodsRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _StoreInoutGoodsRespository.Delete(id);
        }
        public int Update(int id, StoreInoutGoodsUpdateApiModel updated)
        {
            return _StoreInoutGoodsRespository.Update(id, updated);
        }
    }
}
