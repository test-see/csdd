using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("purchase")]
    public class Purchase
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("hospital_department_id")]
        public int HospitalDepartmentId { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("remark")]
        public string Remark { get; set; }
        [Column("status")]
        public int Status { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("createuser_id")]
        public int CreateUserId { get; set; }
        [Column("purchase_setting_id")]
        public int? PurchaseSettingId { get; set; }
    }
}
