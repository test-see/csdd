using foundation.config;
using irespository.invoice.model;
using irespository.store.profile.model;

namespace irespository.invoice
{
    public interface IInvoiceReportRespository
    {
        PagerResult<InvoiceReportListApiModel> GetPagerList(PagerQuery<int> query);
        PagerResult<StoreRecordListApiModel> GetPagerRecordList(PagerQuery<int> query);
        int Generate(int invoiceId);
    }
}
