using foundation.ef5;
using foundation.ef5.poco;
using irespository.client;
using irespository.client.goods.model;
using irespository.client.maping;
using irespository.hospital;
using irespository.hospital.goods.model;
using System;
using System.Linq;

namespace respository.client
{
    public class ClientMappingGoodsRespository : IClientMappingGoodsRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalGoodsRespository _hospitalGoodsRespository;
        private readonly IClientGoodsRespository _clientGoodsRespository;
        public ClientMappingGoodsRespository(DefaultDbContext context,
            IHospitalGoodsRespository hospitalGoodsRespository,
            IClientGoodsRespository clientGoodsRespository)
        {
            _context = context;
            _hospitalGoodsRespository = hospitalGoodsRespository;
            _clientGoodsRespository = clientGoodsRespository;
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
                CreateTime = DateTime.Now,
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

        public ClientMappingGoodsIndexApiModel GetIndexByHospitalGoodsId(int hospitalGoodsId, int clientId)
        {
            var sql = from r in _context.ClientMappingGoods
                      join c in _context.ClientGoods on r.ClientGoodsId equals c.Id
                      where r.HospitalGoodsId == hospitalGoodsId && c.ClientId == clientId
                      select new ClientMappingGoodsIndexApiModel
                      {
                          Id = r.Id,
                          ClientQty = r.ClientQty,
                          HospitalQty = r.HospitalQty,
                          ClientGoods = new ClientGoodsValueModel { Id = r.ClientGoodsId },
                          HospitalGoods = new HospitalGoodsValueModel { Id = r.HospitalGoodsId },
                      };

            var profile = sql.FirstOrDefault();
            if (profile != null)
            {
                profile.HospitalGoods = _hospitalGoodsRespository.GetValue(new int[] { profile.HospitalGoods.Id }).FirstOrDefault();
                profile.ClientGoods = _clientGoodsRespository.GetValue(new int[] { profile.ClientGoods.Id }).FirstOrDefault();
            }
            return profile;
        }

    }
}
