using foundation.config;
using irespository.invoice.model;
using irespository.invoice.report.model;
using irespository.store.profile.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace irespository.invoice
{
    public interface IInvoiceReportRespository
    {
        PagerResult<InvoiceReportListApiModel> GetPagerList(PagerQuery<InvoiceReportQueryApiModel> query);
        Task<PagerResult<StoreRecordListApiModel>> GetPagerRecordListByInvoiceIdAsync(PagerQuery<int> query);
        Task<PagerResult<StoreRecordListApiModel>> GetPagerRecordListByReportIdAsync(PagerQuery<int> query);
        int Generate(int invoiceId, IList<InvoiceReportValueModel> reports);
        List<InvoiceReportValueModel> GetInvoiceListByClient(InvoiceIndexApiModel invoice);
        List<InvoiceReportValueModel> GetInvoiceListByChangeType(InvoiceIndexApiModel invoice);
    }
}
