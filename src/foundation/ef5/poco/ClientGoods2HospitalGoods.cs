using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("client_goods_2hospital_goods")]
    public class ClientGoods2HospitalGoods
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("client_goods_id")]
        public int ClientGoodsId { get; set; }
        [Column("hospital_goods_id")]
        public int HospitalGoodsId { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("createuser_id")]
        public int CreateUserId { get; set; }
        [Column("client_qty")]
        public int ClientQty { get; set; }
        [Column("hospital_qty")]
        public int HospitalQty { get; set; }
    }
}
