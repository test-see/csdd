using foundation.config;
using foundation.ef5.poco;
using irespository.invoice.model;
using irespository.invoice.profile.enums;

namespace irespository.invoice
{
    public interface IInvoiceRespository
    {
        PagerResult<InvoiceListApiModel> GetPagerList(PagerQuery<InvoiceListQueryModel> query);
        Invoice Create(InvoiceCreateApiModel created, int departmentId, int userId);
        int Delete(int id);
        int Update(int id, InvoiceUpdateApiModel updated);
        InvoiceIndexApiModel GetIndex(int id);
        int UpdateStatus(int id, InvoiceStatus status);
    }
}
