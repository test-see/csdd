using foundation.config;
using foundation.ef5.poco;
using irespository.storeinout.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace irespository.storeinout
{
    public interface IStoreInoutGoodsRespository
    {
        Task<PagerResult<StoreInoutGoodsListApiModel>> GetPagerListAsync(PagerQuery<StoreInoutGoodsListQueryModel> query);
        Task<IList<StoreInoutGoodsListApiModel>> GetListByStoreInoutAsync(int storeInoutId);
        StoreInoutGoods Create(StoreInoutGoodsCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, StoreInoutGoodsUpdateApiModel updated);
    }
}
