using foundation.config;
using foundation.ef5.poco;
using irespository.invoice.model;
using irespository.invoice.profile.enums;
using System.Threading.Tasks;

namespace irespository.invoice
{
    public interface IInvoiceRespository
    {
        Task<PagerResult<InvoiceListApiModel>> GetPagerListAsync(PagerQuery<InvoiceListQueryModel> query, int hospitalId);
        Invoice Create(InvoiceCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, InvoiceUpdateApiModel updated);
        Task<InvoiceIndexApiModel> GetIndexAsync(int id);
        int UpdateStatus(int id, InvoiceStatus status);
    }
}
