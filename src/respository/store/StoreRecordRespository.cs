using foundation.config;
using foundation.ef5;
using irespository.hospital;
using irespository.hospital.department.model;
using irespository.hospital.goods.model;
using irespository.hospital.profile.model;
using irespository.store;
using irespository.store.profile.model;
using System.Linq;

namespace respository.store
{
    public class StoreRecordRespository : IStoreRecordRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalGoodsRespository _hospitalGoodsRespository;
        public StoreRecordRespository(DefaultDbContext context,
            IHospitalGoodsRespository hospitalGoodsRespository)
        {
            _context = context;
            _hospitalGoodsRespository = hospitalGoodsRespository;
        }

        public PagerResult<StoreRecordListApiModel> GetPagerList(PagerQuery<StoreRecordListQueryModel> query)
        {
            var sql = from r in _context.StoreRecord
                      join hd in _context.HospitalDepartment on r.HospitalDepartmentId equals hd.Id
                      join hdt in _context.DataDepartmentType on hd.DepartmentTypeId equals hdt.Id
                      join h in _context.Hospital on hd.HospitalId equals h.Id
                      join hg in _context.HospitalGoods on r.HospitalGoodsId equals hg.Id
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
                          },
                      };
            var data = new PagerResult<StoreRecordListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                foreach (var m in data.Result)
                {
                    m.HospitalGoods = _hospitalGoodsRespository.GetValue(m.HospitalGoods.Id);
                }
            }
            return data;
        }
    }
}
