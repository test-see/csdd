using foundation.config;
using foundation.ef5.poco;
using irespository.storeinout.model;
using System.Threading.Tasks;

namespace iservice.store
{
    public interface IStoreInoutGoodsService
    {
        Task<PagerResult<StoreInoutGoodsListApiModel>> GetPagerListAsync(PagerQuery<StoreInoutGoodsListQueryModel> query);
        StoreInoutGoods Create(StoreInoutGoodsCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, StoreInoutGoodsUpdateApiModel updated);
    }
}
