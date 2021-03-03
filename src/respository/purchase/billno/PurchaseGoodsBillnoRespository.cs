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

        public PagerResult<PurchaseGoodsBillnoListApiModel> GetPagerListByHospitalDepartment(PagerQuery<PurchaseGoodsBillnoListQueryModel> query, int hospitalDepartmentId)
        {
            var sql = from r in _context.PurchaseGoodsBillno
                      join p in _context.PurchaseGoods on r.PurchaseGoodsId equals p.Id
                      join x in _context.Purchase on p.PurchaseId equals x.Id
                      join u in _context.User on r.CreateUserId equals u.Id
                      where x.HospitalDepartmentId == hospitalDepartmentId
                      select new PurchaseGoodsBillnoListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Qty = r.Qty,
                          HospitalGoods = new HospitalGoodsValueModel { Id = p.HospitalGoodsId, },
                          Billno = r.Billno,
                          Enddate = r.Enddate,
                          CreateUserName = u.Username,
                          Price = r.Price,
                          Purchase = new PurchaseIndexApiModel { Id = p.PurchaseId, },
                      };

            var data = new PagerResult<PurchaseGoodsBillnoListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                var goods = _hospitalGoodsRespository.GetValue(data.Result.Select(x => x.HospitalGoods.Id).ToArray());
                foreach (var m in data.Result)
                {
                    m.HospitalGoods = goods.FirstOrDefault(x => x.Id == m.HospitalGoods.Id);
                    m.Purchase = _purchaseRespository.GetIndex(m.Purchase.Id);
                }
            }
            return data;
        }

        public PagerResult<PurchaseGoodsBillnoListApiModel> GetPagerListByClient(PagerQuery<PurchaseGoodsBillnoListQueryModel> query, int clientId)
        {
            var sql = from r in _context.PurchaseGoodsBillno
                      join p in _context.PurchaseGoods on r.PurchaseGoodsId equals p.Id
                      join m in _context.ClientMapping on p.HospitalClientId equals m.HospitalClientId
                      join u in _context.User on r.CreateUserId equals u.Id
                      where m.ClientId == clientId
                      select new PurchaseGoodsBillnoListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Qty = r.Qty,
                          HospitalGoods = new HospitalGoodsValueModel { Id = p.HospitalGoodsId, },
                          Billno = r.Billno,
                          Enddate = r.Enddate,
                          CreateUserName = u.Username,
                          Price = r.Price,
                          Purchase = new PurchaseIndexApiModel { Id = p.PurchaseId, },
                      };

            var data = new PagerResult<PurchaseGoodsBillnoListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                var goods = _hospitalGoodsRespository.GetValue(data.Result.Select(x => x.HospitalGoods.Id).ToArray());
                foreach (var m in data.Result)
                {
                    m.HospitalGoods = goods.FirstOrDefault(x => x.Id == m.HospitalGoods.Id);
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
                Price = created.Price,
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
            setting.Price = updated.Price;

            _context.PurchaseGoodsBillno.Update(setting);
            _context.SaveChanges();
            return setting.Id;
        }

        public int UpdateStatus(int id, BillStatus status)
        {
            var setting = _context.PurchaseGoodsBillno.First(x => x.Id == id);
            setting.Status = (int)status;

            _context.PurchaseGoodsBillno.Update(setting);
            _context.SaveChanges();
            return setting.Id;
        }

        public PurchaseGoodsBillnoListApiModel GetIndex(int id)
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
                          Price = r.Price,
                          Purchase = new PurchaseIndexApiModel { Id = p.PurchaseId, },
                      };
            var profile = sql.FirstOrDefault();

            if (profile != null)
            {
                profile.HospitalGoods = _hospitalGoodsRespository.GetValue(new int[] { profile.HospitalGoods.Id }).FirstOrDefault();
                profile.Purchase = _purchaseRespository.GetIndex(profile.Purchase.Id);
            }

            return profile;
        }
    }
}
