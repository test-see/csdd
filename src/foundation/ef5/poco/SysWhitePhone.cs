using Mediator.Net.Contracts;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("sys_white_phone")]
    public class SysWhitePhone:IResponse
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("phone")]
        public string Phone { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("createuser_id")]
        public int? CreateUserId { get; set; }
    }
}
