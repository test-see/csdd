using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("invoice_report_record")]
    public class InvoiceReportRecord
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("invoice_report_id")]
        public int InvoiceReportId { get; set; }
        [Column("store_record_id")]
        public int StoreRecordId { get; set; }
    }
}
