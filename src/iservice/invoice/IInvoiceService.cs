using foundation.config;
using foundation.ef5.poco;
using irespository.invoice.model;
using irespository.invoice.profile.enums;
using irespository.store.profile.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iservice.invoice
{
    public interface IInvoiceService
    {
        Task<PagerResult<InvoiceListApiModel>> GetPagerListAsync(PagerQuery<InvoiceListQueryModel> query, int hospitalId);
        Invoice Create(InvoiceCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, InvoiceUpdateApiModel updated);
        Task<InvoiceIndexApiModel> GetIndexAsync(int id);
        int Submit(int id);
        Task<int> GenerateAsync(int invoiceId);
        PagerResult<InvoiceReportListApiModel> GetPagerReportList(PagerQuery<InvoiceReportQueryApiModel> query);
        Task<PagerResult<StoreRecordListApiModel>> GetPagerRecordListByInvoiceIdAsync(PagerQuery<int> query);
       Task<PagerResult<StoreRecordListApiModel>> GetPagerRecordListByReportIdAsync(PagerQuery<int> query);
        IEnumerable<DataInvoiceType> GetInvoiceTypeList();
    }
}
