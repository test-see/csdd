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
using irespository.store.profile.model;
using System;
using System.Linq;

namespace respository.invoice
{
    public class InvoiceReportRespository : IInvoiceReportRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalClientRespository _hospitalClientRespository;
        private readonly IHospitalGoodsRespository _hospitalGoodsRespository;
        private readonly IHospitalDepartmentRespository _hospitalDepartmentRespository;
        public InvoiceReportRespository(DefaultDbContext context,
            IHospitalClientRespository hospitalClientRespository,
            IHospitalGoodsRespository hospitalGoodsRespository,
            IHospitalDepartmentRespository hospitalDepartmentRespository)
        {
            _context = context;
            _hospitalClientRespository = hospitalClientRespository;
            _hospitalGoodsRespository = hospitalGoodsRespository;
            _hospitalDepartmentRespository = hospitalDepartmentRespository;
        }

        public int Generate(int invoiceId)
        {
            var invoice = _context.Invoice.First(x => x.Id == invoiceId);
            //var sql = from r in _context.StoreRecord
            //          where r.CreateTime > invoice.StartDate && r.CreateTime < invoice.EndDate.Date.AddDays(1);


            //var records = sql.ToList();




            return 0;

        }

        public PagerResult<InvoiceReportListApiModel> GetPagerList(PagerQuery<InvoiceReportQueryApiModel> query)
        {
            var sql = from r in _context.InvoiceReport
                      where r.InvoiceId == query.Query.InvoiceId
                      select new InvoiceReportListApiModel
                      {
                          Id = r.Id,
                          Amount = r.Amount,
                          //Name = 
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
