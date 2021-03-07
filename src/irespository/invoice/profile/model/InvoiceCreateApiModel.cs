using irespository.invoice.profile.enums;
using System;

namespace irespository.invoice.model
{
    public class InvoiceCreateApiModel
    {
        public int HospitalDepartmentId { get; set; }
        public int InvoiceTypeId { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
