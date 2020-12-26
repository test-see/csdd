using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("sys_eventlog")]
    public class SysEventlog
    {
        [Column("id")]
        public int Id { get; set; }
    }
}
