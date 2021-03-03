using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.department.model;
using irespository.hospital.goods.model;
using irespository.hospital.profile.model;
using irespository.store;
using irespository.store.model;
using irespository.store.profile.model;
using irespository.store.record.model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace respository.store
{
    public class StoreRespository : IStoreRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalGoodsRespository _hospitalGoodsRespository;
        private readonly IHospitalDepartmentRespository _hospitalDepartmentRespository;
        private readonly IStoreRecordRespository _storeRecordRespository;
        public StoreRespository(DefaultDbContext context,
            IHospitalGoodsRespository hospitalGoodsRespository,
            IHospitalDepartmentRespository hospitalDepartmentRespository,
            IStoreRecordRespository storeRecordRespository)
        {
            _context = context;
            _hospitalGoodsRespository = hospitalGoodsRespository;
            _hospitalDepartmentRespository = hospitalDepartmentRespository;
            _storeRecordRespository = storeRecordRespository;
        }
        public int CreateOrUpdate(StoreChangeGoodsValueModel created, int changeTypeId, int departmentId, int userId)
        {
            var beforeStore = GetIndexByGoods(departmentId, created.HospitalGoodId);
            var record = _storeRecordRespository.Create(new StoreRecordCreateApiModel
            {
                BeforeQty = beforeStore?.Qty ?? 0,
                ChangeQty = created.Qty,
                ChangeTypeId = changeTypeId,
                HospitalDepartmentId = departmentId,
                HospitalGoodsId = created.HospitalGoodId,
            }, userId);

            if (beforeStore == null) Create(created.HospitalGoodId, created.Qty, departmentId, userId);
            else Update(beforeStore, created.Qty, userId);

            return record.Id;
        }

        private void Create(int hospitalGoodId, int changeQty, int department, int userId)
        {
            var store = new Store
            {
                CreateTime = DateTime.Now,
                CreateUserId = userId,
                HospitalDepartmentId = department,
                HospitalGoodsId = hospitalGoodId,
                Qty = changeQty,
                UpdateTime = DateTime.Now,
                UpdateUserId = userId,
            };
            _context.Store.Add(store);
            _context.SaveChanges();
        }

        private void Update(Store store, int changeQty, int userId)
        {
            store.Qty = +changeQty;
            store.UpdateTime = DateTime.Now;
            store.UpdateUserId = userId;
            _context.Store.Update(store);
            _context.SaveChanges();
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
                var departments = _hospitalDepartmentRespository.GetValue(data.Result.Select(x => x.HospitalDepartment.Id).ToArray());
                foreach (var m in data.Result)
                {
                    m.HospitalDepartment = departments.FirstOrDefault(x => x.Id == m.HospitalDepartment.Id);
                    m.HospitalGoods = _hospitalGoodsRespository.GetValue(m.HospitalGoods.Id);
                }
            }
            return data;
        }
    }
}
