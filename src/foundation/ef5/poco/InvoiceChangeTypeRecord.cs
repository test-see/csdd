using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("invoice_changetype_record")]
    public class InvoiceChangeTypeRecord
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("invoice_changetype_id")]
        public int InvoiceChangeTypeId { get; set; }
        [Column("store_record_id")]
        public int StoreRecordId { get; set; }
    }
}
