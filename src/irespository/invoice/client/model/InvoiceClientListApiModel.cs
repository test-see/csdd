using irespository.hospital.client.model;

namespace irespository.invoice.model
{
    public class InvoiceClientListApiModel
    {
        public int Id { get; set; }
        public HospitalClientValueModel HospitalClient { get; set; }
        public decimal Amount { get; set; }
    }
}
