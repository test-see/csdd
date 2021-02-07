using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("checklist_goods")]
    public class CheckListGoods
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("checklist_id")]
        public int CheckListId { get; set; }
        [Column("hospital_goods_id")]
        public int HospitalGoodsId { get; set; }
        [Column("store_qty")]
        public int StoreQty { get; set; }
        [Column("check_qty")]
        public int CheckQty { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("createuser_id")]
        public int CreateUserId { get; set; }
    }
}
