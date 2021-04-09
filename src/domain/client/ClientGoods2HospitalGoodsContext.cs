using foundation.ef5.poco;
using irespository.client.goods.model;
using irespository.client.maping;
using System.Collections.Generic;

namespace domain.client
{
    public class ClientGoods2HospitalGoodsContext
    {
        private readonly IClientGoods2HospitalGoodsRespository _clientMappingGoodsRespository;
        public ClientGoods2HospitalGoodsContext(IClientGoods2HospitalGoodsRespository clientMappingGoodsRespository)
        {
            _clientMappingGoodsRespository = clientMappingGoodsRespository;
        }

        public ClientGoods2HospitalGoods Create(ClientGoods2HospitalGoodsCreateApiModel created, int userId)
        {
            return _clientMappingGoodsRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _clientMappingGoodsRespository.Delete(id);
        }

        public IList<ClientGoods2HospitalGoodsIndexApiModel> GetIndexByHospitalGoodsId(int[] hospitalGoodsIds, int clientId)
        {
            return _clientMappingGoodsRespository.GetIndexByHospitalGoodsId(hospitalGoodsIds, clientId);
        }
    }
}
