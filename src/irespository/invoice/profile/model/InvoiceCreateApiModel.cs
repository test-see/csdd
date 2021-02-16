using System;

namespace irespository.invoice.model
{
    public class InvoiceCreateApiModel
    {
        public string Name { get; set; }
        public string Remark { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
