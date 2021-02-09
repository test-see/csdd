using foundation.config;
using foundation.ef5.poco;
using irespository.storeinout.model;

namespace iservice.store
{
    public interface IStoreInoutGoodsService
    {
        PagerResult<StoreInoutGoodsListApiModel> GetPagerList(PagerQuery<StoreInoutGoodsListQueryModel> query);
        StoreInoutGoods Create(StoreInoutGoodsCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, StoreInoutGoodsUpdateApiModel updated);
    }
}
