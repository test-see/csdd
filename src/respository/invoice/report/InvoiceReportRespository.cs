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
                      group new { r.Price, r.Id } by new { ht.Id, ht.Name } into gt
                      select new InvoiceReportValueModel
                      {
                          Name = gt.Key.Name,
                          Amount = gt.Sum(x => x.Price),
                          StoreRecordIds = gt.Select(x => x.Id).ToList(),
                      };
            return sql.ToList();
        }

        public List<InvoiceReportValueModel> GetInvoiceListByChangeType(InvoiceIndexApiModel invoice)
        {
            var sql = from r in _context.StoreRecord
                      join t in _context.DataInvoiceType on r.ChangeTypeId equals t.Id
                      where r.CreateTime > invoice.StartDate
                      && r.CreateTime < invoice.EndDate.Date.AddDays(1)
                      && r.HospitalDepartmentId == invoice.HospitalDepartment.Id
                      group new { r.Price, r.Id } by t.Name into gt
                      select new InvoiceReportValueModel
                      {
                          Name = gt.Key,
                          Amount = gt.Sum(x => x.Price),
                          StoreRecordIds = gt.Select(x => x.Id).ToList(),
                      };
            return sql.ToList();
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

        public PagerResult<StoreRecordListApiModel> GetPagerRecordList(PagerQuery<InvoiceReportRecordQueryApiModel> query)
        {
            var sql = from r in _context.StoreRecord
                      join uc in _context.User on r.CreateUserId equals uc.Id
                      join ct in _context.DataStoreChangeType on r.ChangeTypeId equals ct.Id
                      join rts in _context.InvoiceReportRecord on r.Id equals rts.StoreRecordId
                      where rts.InvoiceReportId == query.Query.InvoiceReportId
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
                foreach (var m in data.Result)
                {
                    m.HospitalGoods = _hospitalGoodsRespository.GetValue(m.HospitalGoods.Id);
                    m.HospitalDepartment = _hospitalDepartmentRespository.GetValue(m.HospitalDepartment.Id);
                }
            }
            return data;
        }
    
    }
}
