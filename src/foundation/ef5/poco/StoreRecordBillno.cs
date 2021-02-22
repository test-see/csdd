using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("store_record_billno")]
    public class StoreRecordBillno
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("store_record_id")]
        public int StoreRecordId { get; set; }
        [Column("purchase_goods_billno_id")]
        public int PurchaseGoodsBillnoId { get; set; }
    }
}
