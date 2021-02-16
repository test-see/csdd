using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("invoice_changetype")]
    public class InvoiceChangeType
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("invoice_id")]
        public int InvoiceId { get; set; }
        [Column("changetype_id")]
        public int ChangeTypeId { get; set; }
        [Column("amount")]
        public decimal Amount { get; set; }
    }
}
