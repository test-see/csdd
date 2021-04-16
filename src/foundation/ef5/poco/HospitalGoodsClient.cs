using Mediator.Net.Contracts;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("hospital_goods_client")]
    public class HospitalGoodsClient:IResponse
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("hospital_goods_id")]
        public int HospitalGoodsId { get; set; }
        [Column("hospital_client_id")]
        public int HospitalClientId { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("createuser_id")]
        public int CreateUserId { get; set; }
    }
}
