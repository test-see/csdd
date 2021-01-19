using domain.client;
using foundation.ef5.poco;
using irespository.client.goods.model;
using iservice.client;

namespace service.client
{
    public class ClientMappingGoodsService: IClientMappingGoodsService
    {
        private readonly ClientMappingGoodsContext _clientMappingGoodsContext;
        public ClientMappingGoodsService(ClientMappingGoodsContext clientMappingGoodsContext)
        {
            _clientMappingGoodsContext = clientMappingGoodsContext;
        }
        public ClientMappingGoods Create(ClientMappingGoodsCreateApiModel created, int userId)
        {
            return _clientMappingGoodsContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _clientMappingGoodsContext.Delete(id);
        }
    }
}
