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
        private readonly ClientGoodsContext _ClientGoodsContext;
        public ClientGoodsService(ClientGoodsContext ClientGoodsContext)
        {
            _ClientGoodsContext = ClientGoodsContext;
        }
        public PagerResult<ClientGoodsListApiModel> GetPagerList(PagerQuery<ClientGoodsListQueryModel> query)
        {
            return _ClientGoodsContext.GetPagerList(query);
        }
        public ClientGoods Create(ClientGoodsCreateApiModel created, int userId)
        {
            return _ClientGoodsContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _ClientGoodsContext.Delete(id);
        }

        public int Update(int id, ClientGoodsUpdateApiModel updated, int userId)
        {
            return _ClientGoodsContext.Update(id, updated, userId);
        }

    }
}
