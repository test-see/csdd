﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("purchase_setting")]
    public class PurchaseSetting
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("hospital_department_id")]
        public int HospitalDepartmentId { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("remark")]
        public string Remark { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("createuser_id")]
        public int CreateUserId { get; set; }
    }
}
