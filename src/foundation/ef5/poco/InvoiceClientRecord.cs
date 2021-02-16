using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("invoice_client_record")]
    public class InvoiceClientRecord
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("invoice_client_id")]
        public int InvoiceClientId { get; set; }
        [Column("store_record_id")]
        public int StoreRecordId { get; set; }
    }
}
