using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.goods.model;
using irespository.purchase;
using irespository.purchase.model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace respository.purchase
{
    public class PurchaseSettingThresholdRespository : IPurchaseSettingThresholdRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalGoodsRespository _hospitalGoodsRespository;
        public PurchaseSettingThresholdRespository(DefaultDbContext context,
            IHospitalGoodsRespository hospitalGoodsRespository)
        {
            _context = context;
            _hospitalGoodsRespository = hospitalGoodsRespository;
        }
        public PagerResult<PurchaseSettingThresholdListApiModel> GetPagerList(PagerQuery<PurchaseSettingThresholdListQueryModel> query)
        {
            var sql = from r in _context.PurchaseSettingThreshold
                      join p in _context.PurchaseSetting on r.PurchaseSettingId equals p.Id
                      join u in _context.User on r.CreateUserId equals u.Id
                      join t in _context.DataPurchaseThresholdType on r.ThresholdTypeId equals t.Id
                      select new PurchaseSettingThresholdListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          CreateUserName = u.Username,
                          DownQty = r.DownQty,
                          UpQty = r.UpQty,
                          HospitalGoods = new HospitalGoodsValueModel
                          {
                              Id = r.HospitalGoodsId,
                          },
                          ThresholdType = t,
                      };
            var data = new PagerResult<PurchaseSettingThresholdListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                var goods = _hospitalGoodsRespository.GetValue(data.Result.Select(x => x.HospitalGoods.Id).ToArray());
                foreach (var m in data.Result)
                {
                    m.HospitalGoods = goods.FirstOrDefault(x => x.Id == m.HospitalGoods.Id);
                }
            }
            return data;
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
                ThresholdTypeId = created.ThresholdTypeId,
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
            setting.ThresholdTypeId = updated.ThresholdTypeId;

            _context.PurchaseSettingThreshold.Update(setting);
            _context.SaveChanges();
            return setting.Id;
        }

        public IList<PurchaseSettingThreshold> GetListBySettingId(int settingId)
        {
            return _context.PurchaseSettingThreshold.Where(x => x.PurchaseSettingId == settingId).ToList();
        }
    }
}
