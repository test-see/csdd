using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.goods.model;
using irespository.purchase;
using irespository.purchase.model;
using System;
using System.Linq;

namespace respository.purchase
{
    public class PurchaseGoodsRespository : IPurchaseGoodsRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalGoodsRespository _hospitalGoodsRespository;
        public PurchaseGoodsRespository(DefaultDbContext context,
            IHospitalGoodsRespository hospitalGoodsRespository)
        {
            _context = context;
            _hospitalGoodsRespository = hospitalGoodsRespository;
        }
        public PagerResult<PurchaseGoodsListApiModel> GetPagerList(PagerQuery<PurchaseGoodsListQueryModel> query)
        {
            var sql = from r in _context.PurchaseGoods
                      join t in _context.HospitalClient on r.HospitalClientId equals t.Id
                      select new PurchaseGoodsListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Qty = r.Qty,
                          HospitalGoods = new HospitalGoodsValueModel
                          {
                              Id = r.HospitalGoodsId,
                          },
                          HospitalClient = new IdNameValueModel { Id = t.Id, Name = t.Name, },
                      };
            var data = new PagerResult<PurchaseGoodsListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                foreach (var m in data.Result)
                {
                    m.HospitalGoods = _hospitalGoodsRespository.GetValue(m.HospitalGoods.Id);
                }
            }
            return data;
        }

        public PurchaseGoods Create(PurchaseGoodsCreateApiModel created, int userId)
        {
            var setting = new PurchaseGoods
            {
                PurchaseId = created.PurchaseId,
                HospitalGoodsId = created.HospitalGoodsId,
                Qty = created.Qty,
                CreateTime = DateTime.Now,
                HospitalClientId = created.HospitalClientId,
            };

            _context.PurchaseGoods.Add(setting);
            _context.SaveChanges();

            return setting;
        }

        public int Delete(int id)
        {
            var setting = _context.PurchaseGoods.Find(id);
            _context.PurchaseGoods.Remove(setting);
            _context.SaveChanges();
            return id;
        }

        public int Update(int id, PurchaseGoodsUpdateApiModel updated)
        {
            var setting = _context.PurchaseGoods.First(x => x.Id == id);
            setting.Qty = updated.Qty;
            setting.HospitalClientId = updated.HospitalClientId;

            _context.PurchaseGoods.Update(setting);
            _context.SaveChanges();
            return setting.Id;
        }
    }
}
