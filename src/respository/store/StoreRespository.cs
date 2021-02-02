﻿using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.department.model;
using irespository.hospital.goods.model;
using irespository.hospital.profile.model;
using irespository.store;
using irespository.store.model;
using irespository.store.profile.model;
using System;
using System.Linq;

namespace respository.store
{
    public class StoreRespository : IStoreRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalGoodsRespository _hospitalGoodsRespository;
        private readonly IHospitalDepartmentRespository _hospitalDepartmentRespository;
        public StoreRespository(DefaultDbContext context,
            IHospitalGoodsRespository hospitalGoodsRespository,
            IHospitalDepartmentRespository hospitalDepartmentRespository)
        {
            _context = context;
            _hospitalGoodsRespository = hospitalGoodsRespository;
            _hospitalDepartmentRespository = hospitalDepartmentRespository;
        }
        public Store CreateOrUpdate(CustomizeStoreChangeApiModel created, int department, int userId)
        {
            var store = GetIndexByGoods(department, created.HospitalGoodsId);
            var goods = _context.HospitalGoods.Find(created.HospitalGoodsId);
            var record = new StoreRecord
            {
                BeforeQty = store?.Qty ?? 0,
                ChangeTypeId = created.ChangeTypeId,
                CreateTime = DateTime.Now,
                CreateUserId = userId,
                HospitalDepartmentId = department,
                HospitalGoodsId = created.HospitalGoodsId,
                Price = goods.Price,
                ChangeQty = created.ChangeQty,
            };
            using (var tran = _context.Database.BeginTransaction())
            {
                if (store == null) store = Create(created, department, userId);
                else store = Update(store.Id, created, userId);

                _context.StoreRecord.Add(record);
                _context.SaveChanges();
                tran.Commit();
            }
            return store;
        }

        private Store Create(CustomizeStoreChangeApiModel created, int department, int userId)
        {
            var store = new Store
            {
                CreateTime = DateTime.Now,
                CreateUserId = userId,
                HospitalDepartmentId = department,
                HospitalGoodsId = created.HospitalGoodsId,
                Qty = created.ChangeQty,
                UpdateTime = DateTime.Now,
                UpdateUserId = userId,
            };
            _context.Store.Add(store);
            _context.SaveChanges();
            return store;
        }

        private Store Update(int id, CustomizeStoreChangeApiModel created, int userId)
        {
            var store = _context.Store.Find(id);
            store.Qty = created.ChangeQty + store.Qty;
            store.UpdateTime = DateTime.Now;
            store.UpdateUserId = userId;
            _context.Store.Update(store);
            _context.SaveChanges();
            return store;
        }

        public Store GetIndexByGoods(int department, int goods)
        {
            return _context.Store.FirstOrDefault(x => x.HospitalDepartmentId == department && x.HospitalGoodsId == goods);
        }

        public PagerResult<StoreListApiModel> GetPagerList(PagerQuery<StoreListQueryModel> query)
        {
            var sql = from r in _context.Store
                      join uc in _context.User on r.CreateUserId equals uc.Id
                      join uu in _context.User on r.UpdateUserId equals uu.Id
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
                              Id = r.HospitalDepartmentId,
                          },
                          HospitalGoods = new HospitalGoodsValueModel
                          {
                              Id = r.HospitalGoodsId,
                          },
                      };
            var data = new PagerResult<StoreListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                foreach (var m in data.Result)
                {
                    m.HospitalGoods = _hospitalGoodsRespository.GetValue(m.HospitalGoods.Id);
                    m.HospitalDepartment = _hospitalDepartmentRespository.GetValue(m.HospitalDepartment.Id);
                }
            }
            return data;
        }
    }
}
