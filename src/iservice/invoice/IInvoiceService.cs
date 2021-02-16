using foundation.config;
using foundation.ef5.poco;
using irespository.invoice.model;

namespace iservice.invoice
{
    public interface IInvoiceService
    {
        PagerResult<InvoiceListApiModel> GetPagerList(PagerQuery<InvoiceListQueryModel> query);
        Invoice Create(InvoiceCreateApiModel created, int departmentId, int userId);
        int Delete(int id);
        int Update(int id, InvoiceUpdateApiModel updated);
        InvoiceIndexApiModel GetIndex(int id);
        int Submit(int id);
    }
}
