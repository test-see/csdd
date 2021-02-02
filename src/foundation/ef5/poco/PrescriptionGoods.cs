using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("prescription_goods")]
    public class PrescriptionGoods
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("hospital_goods_id")]
        public int HospitalGoodsId { get; set; }
        [Column("qty")]
        public int Qty { get; set; }
    }
}
