using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("tourist_sales_preference")]
    public class TouristSalesPreference
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("tourist_id")]
        public int TouristId { get; set; }
        [Column("hospital_goods_id")]
        public int HospitalGoodsId { get; set; }
    }
}
