using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("eventlog_hospital_goods")]
    public class EventlogHospitalGoods
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("hospital_goods_id")]
        public int HospitalGoodsId { get; set; }
        [Column("eventlog_id")]
        public int EventlogId { get; set; }
    }
}
