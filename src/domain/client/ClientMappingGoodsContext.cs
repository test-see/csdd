using foundation.ef5.poco;
using irespository.client.goods.model;
using irespository.client.maping;
using System.Collections.Generic;

namespace domain.client
{
    public class ClientMappingGoodsContext
    {
        private readonly IClientMappingGoodsRespository _clientMappingGoodsRespository;
        public ClientMappingGoodsContext(IClientMappingGoodsRespository clientMappingGoodsRespository)
        {
            _clientMappingGoodsRespository = clientMappingGoodsRespository;
        }

        public ClientMappingGoods Create(ClientMappingGoodsCreateApiModel created, int userId)
        {
            return _clientMappingGoodsRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _clientMappingGoodsRespository.Delete(id);
        }

        public IList<ClientMappingGoodsIndexApiModel> GetIndexByHospitalGoodsId(int[] hospitalGoodsIds, int clientId)
        {
            return _clientMappingGoodsRespository.GetIndexByHospitalGoodsId(hospitalGoodsIds, clientId);
        }
    }
}
