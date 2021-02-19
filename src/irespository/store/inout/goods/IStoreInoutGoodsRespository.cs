using foundation.config;
using foundation.ef5.poco;
using irespository.storeinout.model;
using System.Collections.Generic;

namespace irespository.storeinout
{
    public interface IStoreInoutGoodsRespository
    {
        PagerResult<StoreInoutGoodsListApiModel> GetPagerList(PagerQuery<StoreInoutGoodsListQueryModel> query);
        IList<StoreInoutGoodsListApiModel> GetListByStoreInout(int storeInoutId);
        StoreInoutGoods Create(StoreInoutGoodsCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, StoreInoutGoodsUpdateApiModel updated);
    }
}
