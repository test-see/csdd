using domain.client;
using foundation.ef5.poco;
using irespository.client.goods.model;
using iservice.client;

namespace service.client
{
    public class ClientGoods2HospitalGoodsService: IClientGoods2HospitalGoodsService
    {
        private readonly ClientGoods2HospitalGoodsContext _clientMappingGoodsContext;
        public ClientGoods2HospitalGoodsService(ClientGoods2HospitalGoodsContext clientMappingGoodsContext)
        {
            _clientMappingGoodsContext = clientMappingGoodsContext;
        }
        public ClientGoods2HospitalGoods Create(ClientGoods2HospitalGoodsCreateApiModel created, int userId)
        {
            return _clientMappingGoodsContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _clientMappingGoodsContext.Delete(id);
        }
    }
}
