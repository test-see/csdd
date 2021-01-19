using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("hospital_goods")]
    public class HospitalGoods
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("hospital_id")]
        public int HospitalId { get; set; }
        [Column("spec")]
        public string Spec { get; set; }
        [Column("unit_purchase")]
        public string UnitPurchase { get; set; }
        [Column("producer")]
        public string Producer { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("createuser_id")]
        public int CreateUserId { get; set; }
        [Column("is_active")]
        public int IsActive { get; set; }
        [Column("pinshou")]
        public string PinShou { get; set; }
    }
}
