﻿using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.goods.model;
using irespository.hospital.model;
using irespository.hospital.profile.model;
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
                          Hospital = new HospitalValueModel
                          {
                              Id = h.Id,
                              Name = h.Name,
                              Remark = h.Remark,
                          },
                          Producer = r.Producer,
                          Spec = r.Spec,
                          Unit = r.Unit,
                          CreateUserName = u.Username,
                          IsActive = r.IsActive,
                          PinShou = r.PinShou,
                          Price = r.Price,
                          Barcode = r.Barcode,
                      };
            if (query.Query?.HospitalId != null)
            {
                sql = sql.Where(x => x.Hospital.Id == query.Query.HospitalId.Value);
            }
            if (!string.IsNullOrEmpty(query.Query?.PinShou))
            {
                sql = sql.Where(x => x.PinShou.Contains(query.Query.PinShou));
            }
            if (!string.IsNullOrEmpty(query.Query?.Barcode))
            {
                sql = sql.Where(x => x.Barcode.Contains(query.Query.Barcode));
            }
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
                Unit = created.Unit,
                CreateUserId = userId,
                IsActive = 1,
                PinShou = created.PinShou,
                Barcode = created.Barcode,
                Price = created.Price,
                CreateTime = DateTime.Now,
            };

            _context.HospitalGoods.Add(goods);
            _context.SaveChanges();

            return goods;
        }

        public HospitalGoods Get(int id)
        {
            return _context.HospitalGoods.Find(id);
        }

        public int Delete(int id)
        {
            var goods = _context.HospitalGoods.Find(id);
            _context.HospitalGoods.Remove(goods);
            _context.SaveChanges();
            return id;
        }

        public int Update(int id, HospitalGoodsUpdateApiModel updated)
        {
            var goods = _context.HospitalGoods.First(x => x.Id == id);

            goods.Name = updated.Name;
            goods.Producer = updated.Producer;
            goods.Spec = updated.Spec;
            goods.Unit = updated.Unit;
            goods.IsActive = updated.IsActive;
            goods.PinShou = updated.PinShou;
            goods.Price = updated.Price;
            goods.Barcode = updated.Barcode;

            _context.HospitalGoods.Update(goods);
            _context.SaveChanges();
            return goods.Id;
        }

        public HospitalGoodsIndexApiModel GetIndex(int id)
        {
            var sql = from r in _context.HospitalGoods
                      join u in _context.User on r.CreateUserId equals u.Id
                      join h in _context.Hospital on r.HospitalId equals h.Id
                      where r.Id == id
                      select new HospitalGoodsIndexApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Name = r.Name,
                          Hospital = new HospitalValueModel
                          {
                              Id = h.Id,
                              Name = h.Name,
                              Remark = h.Remark,
                          },
                          Producer = r.Producer,
                          Spec = r.Spec,
                          Unit = r.Unit,
                          CreateUserName = u.Username,
                          IsActive = r.IsActive,
                          PinShou = r.PinShou,
                          Price = r.Price,
                      };
            return sql.FirstOrDefault();
        }

        public HospitalGoods UpdateIsActive(int id, bool isActive)
        {
            var goods = _context.HospitalGoods.Where(x => x.Id == id).FirstOrDefault();
            goods.IsActive = isActive ? 1 : 0;
            _context.HospitalGoods.Update(goods);
            _context.SaveChanges();
            return goods;
        }

        public HospitalGoodsValueModel GetValue(int id)
        {
            var sql = from r in _context.HospitalGoods
                      join u in _context.User on r.CreateUserId equals u.Id
                      join h in _context.Hospital on r.HospitalId equals h.Id
                      where r.Id == id
                      select new HospitalGoodsValueModel
                      {
                          Id = r.Id,
                          Name = r.Name,
                          Hospital = new HospitalValueModel
                          {
                              Id = h.Id,
                              Name = h.Name,
                              Remark = h.Remark,
                          },
                          Producer = r.Producer,
                          Spec = r.Spec,
                          Unit = r.Unit,
                          PinShou = r.PinShou,
                          Price = r.Price,
                          Barcode = r.Barcode,
                      };
            return sql.FirstOrDefault();
        }


        public HospitalGoodsValueModel GetValueByBarcode(string barcode)
        {
            var sql = from r in _context.HospitalGoods
                      join u in _context.User on r.CreateUserId equals u.Id
                      join h in _context.Hospital on r.HospitalId equals h.Id
                      where r.Barcode == barcode.Trim()
                      select new HospitalGoodsValueModel
                      {
                          Id = r.Id,
                          Name = r.Name,
                          Hospital = new HospitalValueModel
                          {
                              Id = h.Id,
                              Name = h.Name,
                              Remark = h.Remark,
                          },
                          Producer = r.Producer,
                          Spec = r.Spec,
                          Unit = r.Unit,
                          PinShou = r.PinShou,
                          Price = r.Price,
                          Barcode = r.Barcode,
                      };
            return sql.FirstOrDefault();
        }
    }
}
