﻿using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital.department.model;
using irespository.hospital.goods.model;
using irespository.hospital.profile.model;
using irespository.purchase.model;
using irespository.store;
using irespository.store.profile.model;
using System;
using System.Linq;

namespace respository.store
{
    public class StoreRespository : IStoreRespository
    {
        private readonly DefaultDbContext _context;
        public StoreRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public Store CreateOrUpdate(StoreUpdateApiModel updated, int department, int userId)
        {
            var store = GetIndexByGoods(department, updated.HospitalGoodsId);
            var goods = _context.HospitalGoods.Find(updated.HospitalGoodsId);
            var record = new StoreRecord
            {
                BeforeQty = store?.Qty ?? 0,
                ChangeTypeId = updated.ChangeTypeId,
                CreateTime = DateTime.Now,
                CreateUserId = userId,
                HospitalDepartmentId = department,
                HospitalGoodsId = updated.HospitalGoodsId,
                Price = goods.Price,
            };
            using (var tran = _context.Database.BeginTransaction())
            {
                if (store == null) store = Create(updated, department, userId);
                else store = Update(store.Id, updated, userId);

                record.AfterQty = store.Qty;
                _context.StoreRecord.Add(record);
                _context.SaveChanges();
                tran.Commit();
            }
            return store;
        }

        private Store Create(StoreUpdateApiModel updated, int department, int userId)
        {
            var store = new Store
            {
                CreateTime = DateTime.Now,
                CreateUserId = userId,
                HospitalDepartmentId = department,
                HospitalGoodsId = updated.HospitalGoodsId,
                Qty = updated.Qty,
                UpdateTime = DateTime.Now,
                UpdateUserId = userId,
            };
            _context.Store.Add(store);
            _context.SaveChanges();
            return store;
        }

        private Store Update(int id, StoreUpdateApiModel updated, int userId)
        {
            var store = _context.Store.Find(id);
            store.Qty = updated.Qty;
            store.UpdateTime = DateTime.Now;
            store.UpdateUserId = userId;
            _context.Store.Update(store);
            _context.SaveChanges();
            return store;
        }

        public Store GetIndexByGoods(int department, int goods)
        {
            return _context.Store.First(x => x.HospitalDepartmentId == department && x.HospitalGoodsId == goods);
        }

        public PagerResult<StoreListApiModel> GetPagerList(PagerQuery<StoreListQueryModel> query)
        {
            var sql = from r in _context.Store
                      join hd in _context.HospitalDepartment on r.HospitalDepartmentId equals hd.Id
                      join hdt in _context.DataDepartmentType on hd.DepartmentTypeId equals hdt.Id
                      join h in _context.Hospital on hd.HospitalId equals h.Id
                      join hg in _context.HospitalGoods on r.HospitalGoodsId equals hg.Id
                      join uc in _context.User on r.CreateUserId equals uc.Id
                      join uu in _context.User on r.CreateUserId equals uu.Id
                      select new StoreListApiModel
                      {
                          Id = r.Id,
                          CreateTime = r.CreateTime,
                          CreateUserName = uc.Username,
                          UpdateTime = r.UpdateTime,
                          UpdateUserName = uu.Username,
                          Qty = r.Qty,
                          HospitalDepartment = new HospitalDepartmentValueModel
                          {
                              Id = hd.Id,
                              Name = hd.Name,
                              DepartmentType = hdt,
                              Hospital = new HospitalValueModel
                              {
                                  Id = h.Id,
                                  Name = h.Name,
                                  Remark = h.Remark,
                              },
                          },
                          HospitalGoods = new HospitalGoodsValueModel
                          {
                              Id = hg.Id,
                              Name = hg.Name,
                              PinShou = hg.PinShou,
                              Producer = hg.Producer,
                              UnitPurchase = hg.UnitPurchase,
                              Spec = hg.Spec,
                              Hospital = new HospitalValueModel
                              {
                                  Id = h.Id,
                                  Name = h.Name,
                                  Remark = h.Remark,
                              },
                          },
                      };
            return new PagerResult<StoreListApiModel>(query.Index, query.Size, sql);
        }
    }
}