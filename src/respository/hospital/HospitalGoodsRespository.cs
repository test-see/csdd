﻿using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.goods.model;
using irespository.hospital.model;
using System;
using System.Linq;

namespace respository.hospital
{
    public class HospitalGoodsRespository : IHospitalGoodsRespository
    {
        private readonly DefaultDbContext _context;
        public HospitalGoodsRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public PagerResult<HospitalGoodsListApiModel> GetPagerList(PagerQuery<HospitalGoodsListQueryModel> query)
        {
            var sql = from r in _context.HospitalGoods
                      join u in _context.User on r.CreateUserId equals u.Id
                      join h in _context.Hospital on r.HospitalId equals h.Id
                      select new HospitalGoodsListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Name = r.Name,
                          HospitalName = h.Name,
                          Producer = r.Producer,
                          Spec = r.Spec,
                          UnitPurchase = r.UnitPurchase,
                          CreateUserName = u.Username,
                      };
            return new PagerResult<HospitalGoodsListApiModel>(query.Index, query.Size, sql);
        }

        public HospitalGoods Create(HospitalGoodsCreateApiModel created, int userId)
        {
            var goods = new HospitalGoods
            {
                Name = created.Name,
                HospitalId = created.HospitalId,
                Producer = created.Producer,
                Spec = created.Spec,
                UnitPurchase = created.UnitPurchase,
                CreateUserId = userId,
                CreateTime = DateTime.UtcNow
            };

            _context.HospitalGoods.Add(goods);
            _context.SaveChanges();

            return goods;
        }

        public int Delete(int id)
        {
            var goods = _context.HospitalGoods.Find(id);
            _context.HospitalGoods.Remove(goods);
            _context.SaveChanges();
            return id;
        }

        public int Update(HospitalGoodsUpdateApiModel updated)
        {
            var goods = _context.HospitalGoods.First(x => x.Id == updated.Id);

            goods.Name = updated.Name;
            goods.Producer = updated.Producer;
            goods.Spec = updated.Spec;
            goods.UnitPurchase = updated.UnitPurchase;

            _context.HospitalGoods.Update(goods);
            _context.SaveChanges();
            return goods.Id;
        }
    }
}
