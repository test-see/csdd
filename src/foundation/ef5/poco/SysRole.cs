using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("sys_role")]
    public class SysRole
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
    }
}
