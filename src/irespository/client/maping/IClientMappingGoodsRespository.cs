using foundation.ef5.poco;
using irespository.client.goods.model;

namespace irespository.client.maping
{
    public interface IClientMappingGoodsRespository
    {
        int Delete(int id);
        ClientMappingGoods Create(ClientMappingGoodsCreateApiModel created, int userId);
    }
}
