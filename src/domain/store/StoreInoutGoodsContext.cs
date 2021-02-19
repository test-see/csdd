using foundation.config;
using foundation.ef5.poco;
using irespository.storeinout;
using irespository.storeinout.model;
using System.Collections.Generic;

namespace domain.storeinout
{
    public class StoreInoutGoodsContext
    {
        private readonly IStoreInoutGoodsRespository _StoreInoutGoodsRespository;
        public StoreInoutGoodsContext(IStoreInoutGoodsRespository StoreInoutGoodsRespositoryy)
        {
            _StoreInoutGoodsRespository = StoreInoutGoodsRespositoryy;
        }

        public PagerResult<StoreInoutGoodsListApiModel> GetPagerList(PagerQuery<StoreInoutGoodsListQueryModel> query)
        {
            return _StoreInoutGoodsRespository.GetPagerList(query);
        }
        public IList<StoreInoutGoodsListApiModel> GetListByStoreInout(int storeInoutId)
        {
            return _StoreInoutGoodsRespository.GetListByStoreInout(storeInoutId);
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
