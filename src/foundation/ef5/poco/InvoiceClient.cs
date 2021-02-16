using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("invoice_client")]
    public class InvoiceClient
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("invoice_id")]
        public int InvoiceId { get; set; }
        [Column("hospital_client_id")]
        public int HospitalClientId { get; set; }
        [Column("amount")]
        public decimal Amount { get; set; }
    }
}
