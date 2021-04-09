using foundation.ef5.poco;
using irespository.client.goods.model;

namespace iservice.client
{
    public interface IClientGoods2HospitalGoodsService
    {
        ClientGoods2HospitalGoods Create(ClientGoods2HospitalGoodsCreateApiModel created, int userId);
        int Delete(int id);
    }
}
