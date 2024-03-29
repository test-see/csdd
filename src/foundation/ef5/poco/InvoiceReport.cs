﻿using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("invoice_report")]
    public class InvoiceReport
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("invoice_id")]
        public int InvoiceId { get; set; }
        [Column("amount")]
        public decimal Amount { get; set; }
    }
}
