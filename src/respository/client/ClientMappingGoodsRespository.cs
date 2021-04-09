using foundation.ef5;
using foundation.ef5.poco;
using irespository.client;
using irespository.client.goods.model;
using irespository.client.maping;
using irespository.hospital;
using irespository.hospital.goods.model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace respository.client
{
    public class ClientMappingGoodsRespository : IClientGoods2HospitalGoodsRespository
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
        public ClientGoods2HospitalGoods Create(ClientGoods2HospitalGoodsCreateApiModel created, int userId)
        {
            var mapping = new ClientGoods2HospitalGoods
            {
                ClientGoodsId = created.ClientGoodsId,
                ClientQty = created.ClientQty,
                HospitalGoodsId = created.HospitalGoodsId,
                HospitalQty = created.HospitalQty,
                CreateUserId = userId,
                CreateTime = DateTime.Now,
            };
            _context.ClientGoods2HospitalGoods.Add(mapping);
            _context.SaveChanges();

            return mapping;
        }

        public int Delete(int id)
        {
            var mapping = _context.ClientGoods2HospitalGoods.Find(id);
            _context.ClientGoods2HospitalGoods.Remove(mapping);
            _context.SaveChanges();
            return id;
        }

        public IList<ClientGoods2HospitalGoodsIndexApiModel> GetIndexByHospitalGoodsId(int[] hospitalGoodsIds, int clientId)
        {
            var sql = from r in _context.ClientGoods2HospitalGoods
                      join c in _context.ClientGoods on r.ClientGoodsId equals c.Id
                      where hospitalGoodsIds.Contains(r.HospitalGoodsId) && c.ClientId == clientId
                      select new ClientGoods2HospitalGoodsIndexApiModel
                      {
                          Id = r.Id,
                          ClientQty = r.ClientQty,
                          HospitalQty = r.HospitalQty,
                          ClientGoods = new ClientGoodsValueModel { Id = r.ClientGoodsId },
                          HospitalGoods = new HospitalGoodsValueModel { Id = r.HospitalGoodsId },
                      };

            var data = sql.ToList();
            foreach (var profile in data)
            {
                profile.HospitalGoods = _hospitalGoodsRespository.GetValue(new int[] { profile.HospitalGoods.Id }).FirstOrDefault();
                profile.ClientGoods = _clientGoodsRespository.GetValue(new int[] { profile.ClientGoods.Id }).FirstOrDefault();
            }
            return data;
        }

    }
}
