using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("eventlog")]
    public class Eventlog
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("content")]
        public string Content { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("optionuser_id")]
        public int OptionUserId { get; set; }
    }
}
