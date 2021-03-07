using irespository.invoice.profile.enums;

namespace irespository.invoice.model
{
    public class InvoiceListQueryModel
    {
        public int? HospitalDepartmentId { get; set; }
        public int? Status { get; set; }
        public InvoiceType? Type { get; set; }
    }
}
