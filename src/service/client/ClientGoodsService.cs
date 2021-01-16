using domain.client;
using foundation.config;
using foundation.ef5.poco;
using irespository.client.goods.model;
using irespository.client.model;
using iservice.client;

namespace service.client
{
    public class ClientGoodsService : IClientGoodsService
    {
        private readonly ClientGoodsContext _clientGoodsContext;
        public ClientGoodsService(ClientGoodsContext ClientGoodsContext)
        {
            _clientGoodsContext = ClientGoodsContext;
        }
        public PagerResult<ClientGoodsListApiModel> GetPagerList(PagerQuery<ClientGoodsListQueryModel> query)
        {
            return _clientGoodsContext.GetPagerList(query);
        }
        public ClientGoods Create(ClientGoodsCreateApiModel created, int userId)
        {
            return _clientGoodsContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _clientGoodsContext.Delete(id);
        }

        public int Update(int id, ClientGoodsUpdateApiModel updated, int userId)
        {
            return _clientGoodsContext.Update(id, updated, userId);
        }

        public ClientGoodsIndexApiModel GetIndex(int id)
        {
            return _clientGoodsContext.GetIndex(id);
        }
    }
}
