using foundation.config;
using foundation.ef5.poco;
using irespository.invoice;
using irespository.invoice.model;
using irespository.invoice.profile.enums;

namespace domain.invoice
{
    public class InvoiceContext
    {
        private readonly IInvoiceRespository _InvoiceRespository;
        public InvoiceContext(IInvoiceRespository InvoiceRespository)
        {
            _InvoiceRespository = InvoiceRespository;
        }

        public PagerResult<InvoiceListApiModel> GetPagerList(PagerQuery<InvoiceListQueryModel> query)
        {
            return _InvoiceRespository.GetPagerList(query);
        }
        public Invoice Create(InvoiceCreateApiModel created, int departmentId, int userId)
        {
            return _InvoiceRespository.Create(created, departmentId, userId);
        }
        public int Delete(int id)
        {
            return _InvoiceRespository.Delete(id);
        }
        public int Update(int id, InvoiceUpdateApiModel updated)
        {
            return _InvoiceRespository.Update(id, updated);
        }
        public InvoiceIndexApiModel GetIndex(int id)
        {
            var goods = _InvoiceRespository.GetIndex(id);
            return goods;
        }
        public int Submit(int id)
        {
            return _InvoiceRespository.UpdateStatus(id, InvoiceStatus.Submited);
        }
    }
}
