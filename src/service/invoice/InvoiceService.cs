using domain.invoice;
using foundation.config;
using foundation.ef5.poco;
using irespository.invoice.model;
using irespository.invoice.profile.enums;
using irespository.store.profile.model;
using iservice.invoice;

namespace service.invoice
{
    public class InvoiceService : IInvoiceService
    {
        private readonly InvoiceContext _InvoiceContext;
        public InvoiceService(InvoiceContext InvoiceContext)
        {
            _InvoiceContext = InvoiceContext;
        }
        public PagerResult<InvoiceListApiModel> GetPagerList(PagerQuery<InvoiceListQueryModel> query)
        {
            return _InvoiceContext.GetPagerList(query);
        }
        public Invoice Create(InvoiceCreateApiModel created, int departmentId, int userId)
        {
            return _InvoiceContext.Create(created, departmentId, userId);
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
        public PagerResult<StoreRecordListApiModel> GetPagerReportRecordList(PagerQuery<InvoiceReportRecordQueryApiModel> query)
        {
            return _InvoiceContext.GetPagerReportRecordList(query);
        }
    }
}
