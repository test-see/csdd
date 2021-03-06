using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("client_goods")]
    public class ClientGoods
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("code")]
        public string Code { get; set; }
        [Column("client_id")]
        public int ClientId { get; set; }
        [Column("spec")]
        public string Spec { get; set; }
        [Column("unit")]
        public string Unit { get; set; }
        [Column("producer")]
        public string Producer { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("createuser_id")]
        public int CreateUserId { get; set; }
        [Column("is_active")]
        public int IsActive { get; set; }
    }
}
