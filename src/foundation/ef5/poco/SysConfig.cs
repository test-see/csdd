using Mediator.Net.Contracts;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("sys_config")]
    public class SysConfig:IResponse
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("key")]
        public string Key { get; set; }
        [Column("value")]
        public string Value { get; set; }
        [Column("remark")]
        public string Remark { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("createuser_id")]
        public int CreateUserId { get; set; }
    }
}
