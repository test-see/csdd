using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("invoice")]
    public class Invoice
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("remark")]
        public string Remark { get; set; }
        [Column("startdate")]
        public DateTime StartDate { get; set; }
        [Column("enddate")]
        public DateTime EndDate { get; set; }
        [Column("status")]
        public int Status { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("createuser_id")]
        public int CreateUserId { get; set; }
    }
}
