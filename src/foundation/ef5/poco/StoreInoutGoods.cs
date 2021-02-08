using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("store_inout_goods")]
    public class StoreInoutGoods
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("store_inout_id")]
        public int StoreInoutId { get; set; }
        [Column("hospital_goods_id")]
        public int HospitalGoodsId { get; set; }
        [Column("qty")]
        public int Qty { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
    }
}
