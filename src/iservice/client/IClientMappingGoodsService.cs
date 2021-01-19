using foundation.ef5.poco;
using irespository.client.goods.model;

namespace iservice.client
{
    public interface IClientMappingGoodsService
    {
        ClientMappingGoods Create(ClientMappingGoodsCreateApiModel created, int userId);
        int Delete(int id);
    }
}
