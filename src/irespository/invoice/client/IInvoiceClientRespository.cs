using foundation.config;
using irespository.invoice.model;
using irespository.store.profile.model;

namespace irespository.invoice
{
    public interface IInvoiceClientRespository
    {
        PagerResult<InvoiceClientListApiModel> GetPagerList(PagerQuery<int> query);
        PagerResult<StoreRecordListApiModel> GetPagerRecordList(PagerQuery<int> query);
        int Generate(int invoiceId);
    }
}
