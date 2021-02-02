using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("prescription")]
    public class Prescription
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("cardno")]
        public string Cardno { get; set; }
        [Column("status")]
        public int Status { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("createuser_id")]
        public int? CreateUserId { get; set; }
    }
}
