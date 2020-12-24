using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("sys_privilege")]
    public class SysPrivilege
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("menu_id")]
        public int MenuId { get; set; }
        [Column("role_id")]
        public int RoleId { get; set; }
    }
}
