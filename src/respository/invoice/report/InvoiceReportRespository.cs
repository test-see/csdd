using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.client.model;
using irespository.hospital.department.model;
using irespository.hospital.goods.model;
using irespository.invoice;
using irespository.invoice.model;
using irespository.invoice.profile.enums;
using irespository.invoice.report.model;
using irespository.store.profile.model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace respository.invoice
{
    public class InvoiceReportRespository : IInvoiceReportRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalGoodsRespository _hospitalGoodsRespository;
        private readonly IHospitalDepartmentRespository _hospitalDepartmentRespository;
        public InvoiceReportRespository(DefaultDbContext context,
            IHospitalGoodsRespository hospitalGoodsRespository,
            IHospitalDepartmentRespository hospitalDepartmentRespository)
        {
            _context = context;
            _hospitalGoodsRespository = hospitalGoodsRespository;
            _hospitalDepartmentRespository = hospitalDepartmentRespository;
        }

        public int Generate(int invoiceId, IList<InvoiceReportValueModel> reports)
        {
            if (reports.Any())
            {
                foreach (var report in reports)
                {
                    var d = new InvoiceReport
                    {
                        Amount = report.Amount,
                        InvoiceId = invoiceId,
                        Name = report.Name,
                    };
                    _context.InvoiceReport.Add(d);
                    _context.SaveChanges();

                    var records = report.StoreRecordIds.Select(x => new InvoiceReportRecord
                    {
                        InvoiceReportId = d.Id,
                        StoreRecordId = x
                    });
                    _context.InvoiceReportRecord.AddRange(records);
                    _context.SaveChanges();
                }
            }
            return reports.Count;
        }

        public List<InvoiceReportValueModel> GetInvoiceListByClient(InvoiceIndexApiModel invoice)
        {
            var sql = from r in _context.StoreRecord
                      join b in _context.StoreRecordBillno on r.Id equals b.StoreRecordId
                      join p in _context.PurchaseGoodsBillno on b.PurchaseGoodsBillnoId equals p.Id
                      join c in _context.PurchaseGoods on p.PurchaseGoodsId equals c.Id
                      join ht in _context.HospitalClient on c.HospitalClientId equals ht.Id
                      where r.CreateTime > invoice.StartDate
                      && r.CreateTime < invoice.EndDate.Date.AddDays(1)
                      && r.HospitalDepartmentId == invoice.HospitalDepartment.Id
                      select new
                      {
                          c.HospitalClientId,
                          HospitalClientName = ht.Name,
                          RecordId = r.Id,
                          r.Price
                      };
            var reports = sql.Select(x => new InvoiceReportValueModel
            {
                Id = x.HospitalClientId,
                Name = x.HospitalClientName
            }).Distinct().ToList();
            foreach (var item in reports)
            {
                item.Amount = sql.Where(x => x.HospitalClientId == item.Id).Sum(x => x.Price);
                item.StoreRecordIds = sql.Select(x => x.RecordId).ToList();
            }
            return reports;
        }

        public List<InvoiceReportValueModel> GetInvoiceListByChangeType(InvoiceIndexApiModel invoice)
        {
            var sql = from r in _context.StoreRecord
                      join t in _context.DataInvoiceType on r.ChangeTypeId equals t.Id
                      where r.CreateTime > invoice.StartDate
                      && r.CreateTime < invoice.EndDate.Date.AddDays(1)
                      && r.HospitalDepartmentId == invoice.HospitalDepartment.Id
                      select new
                      {
                          r.ChangeTypeId,
                          ChangeTypeName = t.Name,
                          RecordId = r.Id,
                          r.Price
                      };
            var reports = sql.Select(x => new InvoiceReportValueModel
            {
                Id = x.ChangeTypeId,
                Name = x.ChangeTypeName
            }).Distinct().ToList();
            foreach (var item in reports)
            {
                item.Amount = sql.Where(x => x.ChangeTypeId == item.Id).Sum(x => x.Price);
                item.StoreRecordIds = sql.Select(x => x.RecordId).ToList();
            }
            return reports;
        }

        public PagerResult<InvoiceReportListApiModel> GetPagerList(PagerQuery<InvoiceReportQueryApiModel> query)
        {
            var sql = from r in _context.InvoiceReport
                      where r.InvoiceId == query.Query.InvoiceId
                      select new InvoiceReportListApiModel
                      {
                          Id = r.Id,
                          Amount = r.Amount,
                          Name = r.Name,
                      };
            var data = new PagerResult<InvoiceReportListApiModel>(query.Index, query.Size, sql);
            return data;
        }

        public PagerResult<StoreRecordListApiModel> GetPagerRecordListByReportId(PagerQuery<int> query)
        {
            var sql = from r in _context.StoreRecord
                      join uc in _context.User on r.CreateUserId equals uc.Id
                      join ct in _context.DataStoreChangeType on r.ChangeTypeId equals ct.Id
                      join rts in _context.InvoiceReportRecord on r.Id equals rts.StoreRecordId
                      where rts.InvoiceReportId == query.Query
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
                var departments = _hospitalDepartmentRespository.GetValue(data.Result.Select(x => x.HospitalDepartment.Id).ToArray());
                foreach (var m in data.Result)
                {
                    m.HospitalDepartment = departments.FirstOrDefault(x => x.Id == m.HospitalDepartment.Id);
                    m.HospitalGoods = _hospitalGoodsRespository.GetValue(m.HospitalGoods.Id);
                }
            }
            return data;
        }

        public PagerResult<StoreRecordListApiModel> GetPagerRecordListByInvoiceId(PagerQuery<int> query)
        {
            var invoice = _context.Invoice.First(x => x.Id == query.Query);
            IQueryable<StoreRecordListApiModel> sql;
            if(invoice.InvoiceTypeId == (int)InvoiceType.Client)
            {
                sql = GetPagerRecordListForClientByInvoiceId(invoice);
            }
            else
            {
                sql = GetPagerRecordListForChangeTypeByInvoiceId(invoice);
            }
            var data = new PagerResult<StoreRecordListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                var departments = _hospitalDepartmentRespository.GetValue(data.Result.Select(x => x.HospitalDepartment.Id).ToArray());
                foreach (var m in data.Result)
                {
                    m.HospitalDepartment = departments.FirstOrDefault(x => x.Id == m.HospitalDepartment.Id);
                    m.HospitalGoods = _hospitalGoodsRespository.GetValue(m.HospitalGoods.Id);
                }
            }
            return data;
        }

        private IQueryable<StoreRecordListApiModel> GetPagerRecordListForChangeTypeByInvoiceId(Invoice invoice)
        {
            var enddate = invoice.EndDate.Date.AddDays(1);
            var sql = from r in _context.StoreRecord
                      join uc in _context.User on r.CreateUserId equals uc.Id
                      join ct in _context.DataStoreChangeType on r.ChangeTypeId equals ct.Id
                      where r.CreateTime > invoice.StartDate
                      && r.CreateTime < enddate
                      && r.HospitalDepartmentId == invoice.HospitalDepartmentId
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
            return sql;
        }

        private IQueryable<StoreRecordListApiModel> GetPagerRecordListForClientByInvoiceId(Invoice invoice)
        {
            var enddate = invoice.EndDate.Date.AddDays(1);
            var sql = from r in _context.StoreRecord
                      join b in _context.StoreRecordBillno on r.Id equals b.StoreRecordId
                      join uc in _context.User on r.CreateUserId equals uc.Id
                      join ct in _context.DataStoreChangeType on r.ChangeTypeId equals ct.Id
                      where r.CreateTime > invoice.StartDate
                      && r.CreateTime < enddate
                      && r.HospitalDepartmentId == invoice.HospitalDepartmentId
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
            return sql;
        }
    }
}
