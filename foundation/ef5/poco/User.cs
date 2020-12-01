using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("user")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("phone")]
        public string Phone { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("is_active")]
        public int IsActive { get; set; }
    }
}
