using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.department.model;
using irespository.hospital.goods.model;
using irespository.store;
using irespository.store.profile.model;
using irespository.store.record.model;
using System;
using System.Linq;

namespace respository.store
{
    public class StoreRecordRespository : IStoreRecordRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalGoodsRespository _hospitalGoodsRespository;
        private readonly IHospitalDepartmentRespository _hospitalDepartmentRespository;
        public StoreRecordRespository(DefaultDbContext context,
            IHospitalGoodsRespository hospitalGoodsRespository,
            IHospitalDepartmentRespository hospitalDepartmentRespository)
        {
            _context = context;
            _hospitalGoodsRespository = hospitalGoodsRespository;
            _hospitalDepartmentRespository = hospitalDepartmentRespository;
        }

        public PagerResult<StoreRecordListApiModel> GetPagerList(PagerQuery<StoreRecordListQueryModel> query)
        {
            var sql = from r in _context.StoreRecord
                      join uc in _context.User on r.CreateUserId equals uc.Id
                      join ct in _context.DataStoreChangeType on r.ChangeTypeId equals ct.Id
                      select new StoreRecordListApiModel
                      {
                          Id = r.Id,
                          CreateTime = r.CreateTime,
                          CreateUserName = uc.Username,
                          ChangeQty = r.ChangeQty,
                          BeforeQty = r.BeforeQty,
                          Price = r.Price,
                          ChangeType = ct,
                          HospitalDepartment = new HospitalDepartmentValueModel
                          {
                              Id = r.HospitalDepartmentId,
                          },
                          HospitalGoods = new HospitalGoodsValueModel
                          {
                              Id = r.HospitalGoodsId,
                          },
                      };
            var data = new PagerResult<StoreRecordListApiModel>(query.Index, query.Size, sql);
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

        public StoreRecord Create(StoreRecordCreateApiModel created, int userId)
        {
            var goods = _hospitalGoodsRespository.GetValue(created.HospitalGoodsId);
            var record = new StoreRecord
            {
                BeforeQty = created.BeforeQty,
                ChangeTypeId = created.ChangeTypeId,
                CreateTime = DateTime.Now,
                CreateUserId = userId,
                HospitalDepartmentId = created.HospitalDepartmentId,
                HospitalGoodsId = created.HospitalGoodsId,
                Price = goods.Price,
                ChangeQty = created.ChangeQty,
            };

            _context.StoreRecord.Add(record);
            _context.SaveChanges();

            return record;
        }

        public int GetConsumeAmount(int deparmentId, int goodsId, int days)
        {
            var sql = from r in _context.StoreRecord
                      join ct in _context.DataStoreChangeType on r.ChangeTypeId equals ct.Id
                      where r.HospitalDepartmentId == deparmentId && r.HospitalGoodsId == goodsId
                      && ct.IsConsume == 1
                      && r.CreateTime >= DateTime.Today.AddDays(-days)
                      select r.ChangeQty;

            return sql.Sum();
        }
    }
}
