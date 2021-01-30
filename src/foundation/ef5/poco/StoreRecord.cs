﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("store_record")]
    public class StoreRecord
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("hospital_department_id")]
        public int HospitalDepartmentId { get; set; }
        [Column("hospital_goods_id")]
        public int HospitalGoodsId { get; set; }
        [Column("qty")]
        public int Qty { get; set; }
        [Column("changetype_id")]
        public int ChangeTypeId { get; set; }
        [Column("change_qty")]
        public int ChangeQty { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("createuser_id")]
        public int CreateUserId { get; set; }
    }
}