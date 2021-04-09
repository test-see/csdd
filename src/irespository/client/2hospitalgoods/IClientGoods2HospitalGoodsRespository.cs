using foundation.ef5.poco;
using irespository.client.goods.model;
using System.Collections.Generic;

namespace irespository.client.maping
{
    public interface IClientGoods2HospitalGoodsRespository
    {
        int Delete(int id);
        ClientGoods2HospitalGoods Create(ClientGoods2HospitalGoodsCreateApiModel created, int userId);
        IList<ClientGoods2HospitalGoodsIndexApiModel> GetIndexByHospitalGoodsId(int[] hospitalGoodsIds, int clientId);
    }
}
