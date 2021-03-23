using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("purchase_goods")]
    public class PurchaseGoods
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("purchase_id")]
        public int PurchaseId { get; set; }
        [Column("hospital_goods_id")]
        public int HospitalGoodsId { get; set; }
        [Column("qty")]
        public int Qty { get; set; }
        [Column("hospital_client_id")]
        public int HospitalClientId { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("status")]
        public int Status { get; set; }
    }
}
