using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.goods.model;
using irespository.purchase;
using irespository.purchase.model;
using irespository.purchase.profile.enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace respository.purchase
{
    public class PurchaseGoodsBillnoRespository : IPurchaseGoodsBillnoRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalGoodsRespository _hospitalGoodsRespository;
        private readonly IPurchaseRespository _purchaseRespository;
        public PurchaseGoodsBillnoRespository(DefaultDbContext context,
            IHospitalGoodsRespository hospitalGoodsRespository,
            IPurchaseRespository purchaseRespository)
        {
            _context = context;
            _hospitalGoodsRespository = hospitalGoodsRespository;
            _purchaseRespository = purchaseRespository;
        }

        public PagerResult<PurchaseGoodsBillnoListApiModel> GetPagerList(PagerQuery<PurchaseGoodsBillnoListQueryModel> query)
        {
            var sql = from r in _context.PurchaseGoodsBillno
                      join p in _context.PurchaseGoods on r.PurchaseGoodsId equals p.Id
                      join u in _context.User on r.CreateUserId equals u.Id
                      select new PurchaseGoodsBillnoListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Qty = r.Qty,
                          HospitalGoods = new HospitalGoodsValueModel { Id = p.HospitalGoodsId, },
                          Billno = r.Billno,
                          Enddate = r.Enddate,
                          CreateUserName = u.Username,
                          Purchase = new PurchaseIndexApiModel { Id = p.PurchaseId, },
                      };

            var data = new PagerResult<PurchaseGoodsBillnoListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                foreach (var m in data.Result)
                {
                    m.HospitalGoods = _hospitalGoodsRespository.GetValue(m.HospitalGoods.Id);
                    m.Purchase = _purchaseRespository.GetIndex(m.Purchase.Id);
                }
            }
            return data;
        }

        public PurchaseGoodsBillno Create(PurchaseGoodsBillnoCreateApiModel created, int userId)
        {
            var setting = new PurchaseGoodsBillno
            {
                Qty = created.Qty,
                CreateTime = DateTime.Now,
                Billno = created.Billno,
                Enddate = created.Enddate,
                CreateUserId = userId,
                PurchaseGoodsId = created.PurchaseGoodsId,
                Status = (int)BillStatus.Pendding,
            };

            _context.PurchaseGoodsBillno.Add(setting);
            _context.SaveChanges();

            return setting;
        }

        public int Delete(int id)
        {
            var setting = _context.PurchaseGoodsBillno.Find(id);
            _context.PurchaseGoodsBillno.Remove(setting);
            _context.SaveChanges();
            return id;
        }

        public int Update(int id, PurchaseGoodsBillnoUpdateApiModel updated)
        {
            var setting = _context.PurchaseGoodsBillno.First(x => x.Id == id);
            setting.Qty = updated.Qty;
            setting.Billno = updated.Billno;
            setting.Enddate = updated.Enddate;

            _context.PurchaseGoodsBillno.Update(setting);
            _context.SaveChanges();
            return setting.Id;
        }

        public PurchaseGoodsBillno Get(int id)
        {
            return _context.PurchaseGoodsBillno.FirstOrDefault(x=>x.Id == id);
        }

        public int UpdateStatus(int id, BillStatus status)
        {
            var setting = _context.PurchaseGoodsBillno.First(x => x.Id == id);
            setting.Status = (int)status;

            _context.PurchaseGoodsBillno.Update(setting);
            _context.SaveChanges();
            return setting.Id;
        }
    }
}
