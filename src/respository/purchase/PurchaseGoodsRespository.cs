using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.client;
using irespository.hospital;
using irespository.hospital.client.model;
using irespository.hospital.department.model;
using irespository.hospital.goods.model;
using irespository.hospital.profile.model;
using irespository.purchase;
using irespository.purchase.model;
using irespository.purchase.profile.enums;
using System;
using System.Linq;

namespace respository.purchase
{
    public class PurchaseGoodsRespository : IPurchaseGoodsRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalGoodsRespository _hospitalGoodsRespository;
        private readonly IHospitalClientRespository _hospitalClientRespository;
        private readonly IPurchaseRespository _purchaseRespository;
        public PurchaseGoodsRespository(DefaultDbContext context,
            IHospitalGoodsRespository hospitalGoodsRespository,
            IHospitalClientRespository hospitalClientRespository,
            IPurchaseRespository purchaseRespository)
        {
            _context = context;
            _hospitalGoodsRespository = hospitalGoodsRespository;
            _hospitalClientRespository = hospitalClientRespository;
            _purchaseRespository = purchaseRespository;
        }

        public PagerResult<PurchaseGoodsListApiModel> GetPagerList(PagerQuery<PurchaseGoodsListQueryModel> query)
        {
            var sql = from r in _context.PurchaseGoods
                      select new PurchaseGoodsListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Qty = r.Qty,
                          Purchase = new PurchaseValueModel
                          {
                              Id = r.PurchaseId,
                          },
                          Status = r.Status,
                          HospitalGoods = new HospitalGoodsValueModel { Id = r.HospitalGoodsId, },
                          HospitalClient = new HospitalClientValueModel { Id = r.HospitalClientId },
                      };
            if (query.Query?.HospitalGoodsId != null)
            {
                sql = sql.Where(x => query.Query.HospitalGoodsId.Value == x.HospitalGoods.Id);
            }
            if (query.Query?.Status != null)
            {
                sql = sql.Where(x => query.Query.Status.Value == x.Status);
            }
            if (query.Query?.HospitalClientId != null)
            {
                sql = sql.Where(x => query.Query.HospitalClientId.Value == x.HospitalClient.Id);
            }
            if (query.Query?.HospitalGoodsId != null)
            {
                sql = sql.Where(x => query.Query.HospitalGoodsId.Value == x.HospitalGoods.Id);
            }
            if (query.Query?.PurchaseId != null)
            {
                sql = sql.Where(x => x.Purchase.Id == query.Query.PurchaseId.Value);
            }

            var data = new PagerResult<PurchaseGoodsListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                var goods = _hospitalGoodsRespository.GetValue(data.Result.Select(x => x.HospitalGoods.Id).ToArray());
                var clients = _hospitalClientRespository.GetValue(data.Result.Select(x => x.HospitalClient.Id).ToArray());
                var purachses = _purchaseRespository.GetValue(data.Result.Select(x => x.Purchase.Id).ToArray());
                foreach (var m in data.Result)
                {
                    m.HospitalClient = clients.FirstOrDefault(x => x.Id == m.HospitalClient.Id);
                    m.HospitalGoods = goods.FirstOrDefault(x => x.Id == m.HospitalGoods.Id);
                    m.Purchase = purachses.FirstOrDefault(x => x.Id == m.Purchase.Id);
                }
            }
            return data;
        }


        public PagerResult<PurchaseGoodsListApiModel> GetPagerListByClient(PagerQuery<PurchaseGoodsListQueryModel> query, int clientId)
        {
            var sql = from r in _context.PurchaseGoods
                      join x in _context.Purchase on r.PurchaseId equals x.Id
                      join d in _context.HospitalDepartment on x.HospitalDepartmentId equals d.Id
                      join g in _context.ClientMapping on r.HospitalClientId equals g.HospitalClientId
                      where g.ClientId == clientId
                      select new PurchaseGoodsListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Qty = r.Qty,
                          Purchase = new PurchaseValueModel
                          {
                              Id = r.PurchaseId,
                              HospitalDepartment = new HospitalDepartmentValueModel
                              {
                                  Hospital = new HospitalValueModel { Id = d.HospitalId },
                                  Id = d.Id
                              }
                          },
                          Status = r.Status,
                          HospitalGoods = new HospitalGoodsValueModel { Id = r.HospitalGoodsId, },
                          HospitalClient = new HospitalClientValueModel { Id = r.HospitalClientId },
                      };
            if (query.Query?.Status != null)
            {
                sql = sql.Where(x => query.Query.Status.Value == x.Status);
            }
            if (query.Query?.PurchaseId != null)
            {
                sql = sql.Where(x => x.Purchase.Id == query.Query.PurchaseId.Value);
            }
            if (query.Query?.HospitalId != null)
            {
                sql = sql.Where(x => x.Purchase.HospitalDepartment.Hospital.Id == query.Query.HospitalId.Value);
            }
            if (query.Query?.HospitalGoodsId != null)
            {
                sql = sql.Where(x => x.HospitalGoods.Id == query.Query.HospitalGoodsId.Value);
            }
            if (query.Query?.HospitalClientId != null)
            {
                sql = sql.Where(x => query.Query.HospitalClientId.Value == x.HospitalClient.Id);
            }

            var data = new PagerResult<PurchaseGoodsListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                var goods = _hospitalGoodsRespository.GetValue(data.Result.Select(x => x.HospitalGoods.Id).ToArray());
                var clients = _hospitalClientRespository.GetValue(data.Result.Select(x => x.HospitalClient.Id).ToArray());
                var purachses = _purchaseRespository.GetValue(data.Result.Select(x => x.Purchase.Id).ToArray());
                foreach (var m in data.Result)
                {
                    m.HospitalClient = clients.FirstOrDefault(x => x.Id == m.HospitalClient.Id);
                    m.HospitalGoods = goods.FirstOrDefault(x => x.Id == m.HospitalGoods.Id);
                    m.Purchase = purachses.FirstOrDefault(x => x.Id == m.Purchase.Id);
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
                Status = (int)PurchaseGoodsStatus.Pendding,
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
            setting.UpdateTime = DateTime.Now;

            _context.PurchaseGoods.Update(setting);
            _context.SaveChanges();
            return setting.Id;
        }

        public PurchaseGoodsListApiModel GetIndex(int id)
        {
            var sql = from r in _context.PurchaseGoods
                      where r.Id == id
                      select new PurchaseGoodsListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Qty = r.Qty,
                          Purchase = new PurchaseValueModel
                          {
                              Id = r.PurchaseId,
                          },
                          Status = r.Status,
                          HospitalGoods = new HospitalGoodsValueModel { Id = r.HospitalGoodsId, },
                          HospitalClient = new HospitalClientValueModel { Id = r.HospitalClientId },
                      };
            var data = sql.FirstOrDefault();
            if (data!=null)
            {
                data.HospitalGoods = _hospitalGoodsRespository.GetValue(new int[] { data.HospitalGoods.Id }).FirstOrDefault();
                data.HospitalClient = _hospitalClientRespository.GetValue(new int[] { data.HospitalClient.Id }).FirstOrDefault();
                data.Purchase = _purchaseRespository.GetValue(new int[] { data.Purchase.Id }).FirstOrDefault();
            }
            return data;
        }
        
        public int UpdateStatus(int id, PurchaseGoodsStatus status)
        {
            var setting = _context.PurchaseGoods.First(x => x.Id == id);
            setting.Status = (int)status;
            setting.UpdateTime = DateTime.Now;

            _context.PurchaseGoods.Update(setting);
            _context.SaveChanges();
            return setting.Id;
        }
    }
}
