using foundation.ef5;
using foundation.ef5.poco;
using irespository.client.goods.model;
using irespository.client.maping;

namespace respository.client
{
    public class ClientMappingGoodsRespository : IClientMappingGoodsRespository
    {
        private readonly DefaultDbContext _context;
        public ClientMappingGoodsRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public ClientMappingGoods Create(ClientMappingGoodsCreateApiModel created, int userId)
        {
            var mapping = new ClientMappingGoods
            {
                ClientGoodsId = created.ClientGoodsId,
                ClientQty = created.ClientQty,
                HospitalGoodsId = created.HospitalGoodsId,
                HospitalQty = created.HospitalQty,
                CreateUserId = userId,
            };
            _context.ClientMappingGoods.Add(mapping);
            _context.SaveChanges();

            return mapping;
        }

        public int Delete(int id)
        {
            var mapping = _context.ClientMappingGoods.Find(id);
            _context.ClientMappingGoods.Remove(mapping);
            _context.SaveChanges();
            return id;
        }
    }
}
