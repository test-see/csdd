using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital.department.model;
using irespository.hospital.goods.model;
using irespository.hospital.profile.model;
using irespository.purchase;
using irespository.purchase.model;
using System;
using System.Linq;

namespace respository.purchase
{
    public class PurchaseSettingThresholdRespository : IPurchaseSettingThresholdRespository
    {
        private readonly DefaultDbContext _context;
        public PurchaseSettingThresholdRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public PagerResult<PurchaseSettingThresholdListApiModel> GetPagerList(PagerQuery<PurchaseSettingThresholdListQueryModel> query)
        {
            var sql = from r in _context.PurchaseSettingThreshold
                      join g in _context.HospitalGoods on r.HospitalGoodsId equals g.Id
                      join h in _context.Hospital on g.HospitalId equals h.Id
                      join u in _context.User on r.CreateUserId equals u.Id
                      select new PurchaseSettingThresholdListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          CreateUserName = u.Username,
                          DownQty = r.DownQty,
                          UpQty = r.UpQty,
                          HospitalGoods = new HospitalGoodsValueModel
                          {
                              Id = g.Id,
                              Name = g.Name,
                              PinShou = g.PinShou,
                              Producer = g.Producer,
                              Spec = g.Spec,
                              UnitPurchase = g.UnitPurchase,
                              Hospital = new HospitalValueModel
                              {
                                  Id = h.Id,
                                  Name = h.Name,
                                  Remark = h.Remark,
                              }
                          },
                      };
            return new PagerResult<PurchaseSettingThresholdListApiModel>(query.Index, query.Size, sql);
        }

        public PurchaseSettingThreshold Create(PurchaseSettingThresholdCreateApiModel created, int userId)
        {
            var setting = new PurchaseSettingThreshold
            {
                PurchaseSettingId = created.PurchaseSettingId,
                HospitalGoodsId = created.HospitalGoodsId,
                DownQty = created.DownQty,
                UpQty = created.UpQty,
                CreateUserId = userId,
                CreateTime = DateTime.Now,
            };

            _context.PurchaseSettingThreshold.Add(setting);
            _context.SaveChanges();

            return setting;
        }

        public int Delete(int id)
        {
            var setting = _context.PurchaseSettingThreshold.Find(id);
            _context.PurchaseSettingThreshold.Remove(setting);
            _context.SaveChanges();
            return id;
        }

        public int Update(int id, PurchaseSettingThresholdUpdateApiModel updated)
        {
            var setting = _context.PurchaseSettingThreshold.First(x => x.Id == id);
            setting.DownQty = updated.DownQty;
            setting.UpQty = updated.UpQty;

            _context.PurchaseSettingThreshold.Update(setting);
            _context.SaveChanges();
            return setting.Id;
        }
    }
}
