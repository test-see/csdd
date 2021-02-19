using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("purchase_goods_billno")]
    public class PurchaseGoodsBillno
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("purchase_goods_id")]
        public int PurchaseGoodsId { get; set; }
        [Column("billno")]
        public string Billno { get; set; }
        [Column("qty")]
        public int Qty { get; set; }
        [Column("enddate")]
        public DateTime Enddate { get; set; }
        [Column("status")]
        public int Status { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("createuser_id")]
        public int CreateUserId { get; set; }
    }
}
