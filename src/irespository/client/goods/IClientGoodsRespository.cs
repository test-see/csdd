using foundation.config;
using foundation.ef5.poco;
using irespository.client.goods.model;
using irespository.client.model;
using System.Collections.Generic;

namespace irespository.client
{
    public interface IClientGoodsRespository
    {
        PagerResult<ClientGoodsListApiModel> GetPagerList(PagerQuery<ClientGoodsListQueryModel> query);
        ClientGoods Create(ClientGoodsCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, ClientGoodsUpdateApiModel updated, int userId);
        ClientGoodsIndexApiModel GetIndex(int id);
        IList<ClientGoodsValueModel> GetValue(int[] ids);
    }
}
