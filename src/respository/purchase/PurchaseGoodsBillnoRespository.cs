using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.department.model;
using irespository.hospital.goods.model;
using irespository.hospital.profile.model;
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

        public PagerResult<PurchaseGoodsBillnoListApiModel> GetPagerListByHospital(PagerQuery<PurchaseGoodsBillnoListQueryModel> query, int hospitalId)
        {
            var sql = from r in _context.PurchaseGoodsBillno
                      join p in _context.PurchaseGoods on r.PurchaseGoodsId equals p.Id
                      join x in _context.Purchase on p.PurchaseId equals x.Id
                      join d in _context.HospitalDepartment on x.HospitalDepartmentId equals d.Id
                      join u in _context.User on r.CreateUserId equals u.Id
                      where d.HospitalId == hospitalId
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
                          HospitalClientId = p.HospitalClientId,
                          Status = r.Status,
                          Purchase = new PurchaseValueModel
                          {
                              Id = p.PurchaseId,
                              HospitalDepartment = new HospitalDepartmentValueModel
                              {
                                  Id = x.HospitalDepartmentId,
                              }
                          },
                      };
            sql = GetQueryableForList(sql, query.Query);
            var data = new PagerResult<PurchaseGoodsBillnoListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                var goods = _hospitalGoodsRespository.GetValue(data.Result.Select(x => x.HospitalGoods.Id).ToArray());
                var purachses = _purchaseRespository.GetValue(data.Result.Select(x => x.Purchase.Id).ToArray());
                foreach (var m in data.Result)
                {
                    m.HospitalGoods = goods.FirstOrDefault(x => x.Id == m.HospitalGoods.Id);
                    m.Purchase = purachses.FirstOrDefault(x => x.Id == m.Purchase.Id);
                }
            }
            return data;
        }

        public PagerResult<PurchaseGoodsBillnoListApiModel> GetPagerListByClient(PagerQuery<PurchaseGoodsBillnoListQueryModel> query, int clientId)
        {
            var sql = from r in _context.PurchaseGoodsBillno
                      join p in _context.PurchaseGoods on r.PurchaseGoodsId equals p.Id
                      join x in _context.Purchase on p.PurchaseId equals x.Id
                      join d in _context.HospitalDepartment on x.HospitalDepartmentId equals d.Id
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
                          Purchase = new PurchaseValueModel
                          {
                              Id = p.PurchaseId,
                              HospitalDepartment = new HospitalDepartmentValueModel
                              {
                                  Hospital = new HospitalValueModel { Id = d.HospitalId },
                                  Id = d.Id
                              }
                          },
                      };
            sql = GetQueryableForList(sql, query.Query);
            var data = new PagerResult<PurchaseGoodsBillnoListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                var goods = _hospitalGoodsRespository.GetValue(data.Result.Select(x => x.HospitalGoods.Id).ToArray());
                var purachses = _purchaseRespository.GetValue(data.Result.Select(x => x.Purchase.Id).ToArray());
                foreach (var m in data.Result)
                {
                    m.HospitalGoods = goods.FirstOrDefault(x => x.Id == m.HospitalGoods.Id);
                    m.Purchase = purachses.FirstOrDefault(x => x.Id == m.Purchase.Id);
                }
            }
            return data;
        }

        private IQueryable<PurchaseGoodsBillnoListApiModel> GetQueryableForList(IQueryable<PurchaseGoodsBillnoListApiModel> sql ,PurchaseGoodsBillnoListQueryModel query)
        {
            if (query?.HospitalId != null)
            {
                sql = sql.Where(x => x.Purchase.HospitalDepartment.Hospital.Id == query.HospitalId.Value);
            }
            if (query?.HospitalDepartmentId != null)
            {
                sql = sql.Where(x => x.Purchase.HospitalDepartment.Id == query.HospitalDepartmentId.Value);
            }
            if (query?.HospitalGoodsId != null)
            {
                sql = sql.Where(x => x.HospitalGoods.Id == query.HospitalGoodsId.Value);
            }
            if (query?.HospitalClientId != null)
            {
                sql = sql.Where(x => x.HospitalClientId == query.HospitalClientId.Value);
            }
            if (!string.IsNullOrEmpty(query?.Billno))
            {
                sql = sql.Where(x => x.Billno.Contains(query.Billno));
            }
            if (query?.BeginDate != null)
            {
                sql = sql.Where(x => x.CreateTime >= query.BeginDate.Value);
            }
            if (query?.EndDate != null)
            {
                sql = sql.Where(x => x.CreateTime < query.EndDate.Value.AddDays(1));
            }
            if (query?.Status != null)
            {
                sql = sql.Where(x => x.Status == query.Status.Value);
            }
            return sql;
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
                          Purchase = new PurchaseValueModel { Id = p.PurchaseId, },
                      };
            var profile = sql.FirstOrDefault();

            if (profile != null)
            {
                profile.HospitalGoods = _hospitalGoodsRespository.GetValue(new int[] { profile.HospitalGoods.Id }).FirstOrDefault();
                profile.Purchase = _purchaseRespository.GetValue(new int[] { profile.Purchase.Id }).FirstOrDefault();
            }

            return profile;
        }
    }
}
