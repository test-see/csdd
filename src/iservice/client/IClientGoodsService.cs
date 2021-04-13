using foundation.config;
using foundation.ef5.poco;
using irespository.client.goods.model;
using irespository.client.model;

namespace iservice.client
{
    public interface IClientGoodsService
    {
        PagerResult<ClientGoodsListApiModel> GetPagerList(PagerQuery<ClientGoodsListQueryModel> query);
        ClientGoods Create(CreateClientGoods created, int userId);
        int Delete(int id);
        int Update(int id, UpdateClientGoods updated, int userId);
        ClientGoodsIndexApiModel GetIndex(int id);
    }
}
