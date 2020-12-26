using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("user")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("username")]
        public string Username { get; set; }
        [Column("phone")]
        public string Phone { get; set; }
        [Column("is_active")]
        public int IsActive { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("createuser_id")]
        public int? CreateUserId { get; set; }
    }
}
