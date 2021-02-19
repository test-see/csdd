using foundation.config;
using irespository.invoice.model;
using irespository.store.profile.model;

namespace irespository.invoice
{
    public interface IInvoiceReportRespository
    {
        PagerResult<InvoiceReportListApiModel> GetPagerList(PagerQuery<InvoiceReportQueryApiModel> query);
        PagerResult<StoreRecordListApiModel> GetPagerRecordList(PagerQuery<InvoiceReportRecordQueryApiModel> query);
        int Generate(int invoiceId);
    }
}
