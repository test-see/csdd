using foundation.ef5.poco;
using irespository.client.goods.model;
using irespository.client.maping;

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

        public ClientMappingGoodsIndexApiModel GetIndexByHospitalGoodsId(int hospitalGoodsId, int clientId)
        {
            return _clientMappingGoodsRespository.GetIndexByHospitalGoodsId(hospitalGoodsId, clientId);
        }
    }
}
