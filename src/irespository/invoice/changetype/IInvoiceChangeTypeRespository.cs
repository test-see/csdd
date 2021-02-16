using foundation.config;
using irespository.invoice.model;
using irespository.store.profile.model;

namespace irespository.invoice
{
    public interface IInvoiceChangeTypeRespository
    {
        PagerResult<InvoiceChangeTypeListApiModel> GetPagerList(PagerQuery<int> query);
        PagerResult<StoreRecordListApiModel> GetPagerRecordList(int invoiceClientId);
        int Generate(int invoiceId);
    }
}
