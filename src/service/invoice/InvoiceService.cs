using domain.invoice;
using foundation.config;
using foundation.ef5.poco;
using irespository.invoice.model;
using irespository.invoice.profile.enums;
using irespository.store.profile.model;
using iservice.invoice;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace service.invoice
{
    public class InvoiceService : IInvoiceService
    {
        private readonly InvoiceContext _InvoiceContext;
        public InvoiceService(InvoiceContext InvoiceContext)
        {
            _InvoiceContext = InvoiceContext;
        }
        public async Task<PagerResult<InvoiceListApiModel>> GetPagerListAsync(PagerQuery<InvoiceListQueryModel> query, int hospitalId)
        {
            return await _InvoiceContext.GetPagerListAsync(query, hospitalId);
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

        public async Task<InvoiceIndexApiModel> GetIndexAsync(int id)
        {
            return await _InvoiceContext.GetIndexAsync(id);
        }

        public int Submit(int id)
        {
            return _InvoiceContext.Submit(id);
        }


        public async Task<int> GenerateAsync(int invoiceId)
        {
            return await _InvoiceContext.GenerateAsync(invoiceId);
        }


        public PagerResult<InvoiceReportListApiModel> GetPagerReportList(PagerQuery<InvoiceReportQueryApiModel> query)
        {
            return _InvoiceContext.GetPagerReportList(query);
        }
        public async Task<PagerResult<StoreRecordListApiModel>> GetPagerRecordListByInvoiceIdAsync(PagerQuery<int> query)
        {
            return await _InvoiceContext.GetPagerRecordListByInvoiceIdAsync(query);
        }
        public async Task<PagerResult<StoreRecordListApiModel>> GetPagerRecordListByReportIdAsync(PagerQuery<int> query)
        {
            return await _InvoiceContext.GetPagerRecordListByReportIdAsync(query);
        }
        public IEnumerable<DataInvoiceType> GetInvoiceTypeList()
        {
            return _InvoiceContext.GetInvoiceTypeList();
        }
    }
}
