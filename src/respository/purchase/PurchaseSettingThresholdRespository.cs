using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital;
using irespository.hospital.goods.model;
using irespository.purchase;
using irespository.purchase.model;
using Mediator.Net;
using storage.hospitalgoods.carrier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace respository.purchase
{
    public class PurchaseSettingThresholdRespository : IPurchaseSettingThresholdRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IMediator _mediator;
        public PurchaseSettingThresholdRespository(DefaultDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<PagerResult<PurchaseSettingThresholdListApiModel>> GetPagerListAsync(PagerQuery<PurchaseSettingThresholdListQueryModel> query)
        {
            var sql = from r in _context.PurchaseSettingThreshold
                      join p in _context.PurchaseSetting on r.PurchaseSettingId equals p.Id
                      join u in _context.User on r.CreateUserId equals u.Id
                      join t in _context.DataPurchaseThresholdType on r.ThresholdTypeId equals t.Id
                      orderby r.Id descending
                      select new PurchaseSettingThresholdListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          CreateUserName = u.Username,
                          DownQty = r.DownQty,
                          UpQty = r.UpQty,
                          HospitalGoods = new GetHospitalGoodsResponse
                          {
                              Id = r.HospitalGoodsId,
                          },
                          ThresholdType = t,
                      };
            var data = new PagerResult<PurchaseSettingThresholdListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                var goods = await _mediator.ListByIdsAsync<GetHospitalGoodsRequest, GetHospitalGoodsResponse>(data.Select(x => x.HospitalGoods.Id).ToList());
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
