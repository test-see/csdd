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
        [Column("code")]
        public string Code { get; set; }
        [Column("hospital_id")]
        public int HospitalId { get; set; }
        [Column("spec")]
        public string Spec { get; set; }
        [Column("unit")]
        public string Unit { get; set; }
        [Column("producer")]
        public string Producer { get; set; }
        [Column("pinshou")]
        public string PinShou { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
        [Column("is_active")]
        public int IsActive { get; set; }
        [Column("createuser_id")]
        public int CreateUserId { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("barcode")]
        public string Barcode { get; set; }
    }
}
