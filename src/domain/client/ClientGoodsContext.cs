using foundation.config;
using foundation.ef5.poco;
using irespository.client;
using irespository.client.goods.model;
using irespository.client.model;

namespace domain.client
{
    public class ClientGoodsContext
    {
        private readonly IClientGoodsRespository _clientGoodsRespository;
        public ClientGoodsContext(IClientGoodsRespository ClientGoodsRespository)
        {
            _clientGoodsRespository = ClientGoodsRespository;
        }

        public PagerResult<ClientGoodsListApiModel> GetPagerList(PagerQuery<ClientGoodsListQueryModel> query)
        {
            return _clientGoodsRespository.GetPagerList(query);
        }
        public ClientGoods Create(ClientGoodsCreateApiModel created, int userId)
        {
            return _clientGoodsRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _clientGoodsRespository.Delete(id);
        }
        public int Update(int id, ClientGoodsUpdateApiModel updated, int userId)
        {
            _clientGoodsRespository.Update(id, updated, userId);
            return id;
        }

        public ClientGoodsIndexApiModel GetIndex(int id)
        {
            return _clientGoodsRespository.GetIndex(id);
        }
    }
}
