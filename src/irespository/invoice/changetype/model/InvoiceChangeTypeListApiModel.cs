using foundation.config;

namespace irespository.invoice.model
{
    public class InvoiceChangeTypeListApiModel
    {
        public int Id { get; set; }
        public IdNameValueModel ChangeType { get; set; }
        public decimal Amount { get; set; }
    }
}
