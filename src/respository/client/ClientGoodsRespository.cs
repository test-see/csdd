using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.client;
using irespository.client.goods.model;
using irespository.client.model;
using irespository.hospital;
using irespository.hospital.goods.model;
using nouns.client.profile;
using System;
using System.Collections.Generic;
using System.Linq;

namespace respository.client
{
    public class ClientGoodsRespository : IClientGoodsRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalGoodsRespository _hospitalGoodsRespository;
        public ClientGoodsRespository(DefaultDbContext context,
            IHospitalGoodsRespository hospitalGoodsRespository)
        {
            _context = context;
            _hospitalGoodsRespository = hospitalGoodsRespository;
        }

        public ClientGoods Create(CreateClientGoods created, int userId)
        {
            var goods = new ClientGoods
            {
                Code = created.Code,
                Name = created.Name,
                ClientId = created.ClientId,
                Producer = created.Producer,
                Spec = created.Spec,
                Unit = created.Unit,
                CreateUserId = userId,
                IsActive = 1,
                CreateTime = DateTime.Now,
            };

            _context.ClientGoods.Add(goods);
            _context.SaveChanges();

            return goods;
        }

        public int Delete(int id)
        {
            var mappings = _context.ClientGoods2HospitalGoods.Where(x => x.ClientGoodsId == id);
            _context.ClientGoods2HospitalGoods.RemoveRange(mappings);

            var goods = _context.ClientGoods.Find(id);
            _context.ClientGoods.Remove(goods);
            _context.SaveChanges();
            return id;
        }

        public int Update(int id, UpdateClientGoods updated, int userId)
        {
            var goods = _context.ClientGoods.First(x => x.Id == id);

            goods.Code = updated.Code;
            goods.Name = updated.Name;
            goods.Producer = updated.Producer;
            goods.Spec = updated.Spec;
            goods.Unit = updated.Unit;
            goods.IsActive = updated.IsActive;

            _context.ClientGoods.Update(goods);
            _context.SaveChanges();


            return goods.Id;
        }

    }
}
