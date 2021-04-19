using domain.invoice;
using foundation.config;
using foundation.ef5.poco;
using irespository.invoice.model;
using irespository.invoice.profile.enums;
using irespository.store.profile.model;
using iservice.invoice;
using System.Collections.Generic;

namespace service.invoice
{
    public class InvoiceService : IInvoiceService
    {
        private readonly InvoiceContext _InvoiceContext;
        public InvoiceService(InvoiceContext InvoiceContext)
        {
            _InvoiceContext = InvoiceContext;
        }
        public PagerResult<InvoiceListApiModel> GetPagerList(PagerQuery<InvoiceListQueryModel> query, int hospitalId)
        {
            return _InvoiceContext.GetPagerList(query, hospitalId);
        }
        public Invoice Create(InvoiceCreateApiModel created, int userId)
        {
            return _InvoiceContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _InvoiceContext.Delete(id);
        }

        public int Update(int id, InvoiceUpdateApiModel updated)
        {
            return _InvoiceContext.Update(id, updated);
        }

        public InvoiceIndexApiModel GetIndex(int id)
        {
            return _InvoiceContext.GetIndex(id);
        }

        public int Submit(int id)
        {
            return _InvoiceContext.Submit(id);
        }


        public int Generate(int invoiceId)
        {
            return _InvoiceContext.Generate(invoiceId);
        }


        public PagerResult<InvoiceReportListApiModel> GetPagerReportList(PagerQuery<InvoiceReportQueryApiModel> query)
        {
            return _InvoiceContext.GetPagerReportList(query);
        }
        public PagerResult<StoreRecordListApiModel> GetPagerRecordListByInvoiceId(PagerQuery<int> query)
        {
            return _InvoiceContext.GetPagerRecordListByInvoiceIdAsync(query);
        }
        public PagerResult<StoreRecordListApiModel> GetPagerRecordListByReportId(PagerQuery<int> query)
        {
            return _InvoiceContext.GetPagerRecordListByReportIdAsync(query);
        }
        public IEnumerable<DataInvoiceType> GetInvoiceTypeList()
        {
            return _InvoiceContext.GetInvoiceTypeList();
        }
    }
}
