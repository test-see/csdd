﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("purchase_setting_threshold")]
    public class PurchaseSettingThreshold
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("purchase_setting_id")]
        public int PurchaseSettingId { get; set; }
        [Column("hospital_goods_id")]
        public int HospitalGoodsId { get; set; }
        [Column("up_qty")]
        public int UpQty { get; set; }
        [Column("down_qty")]
        public int DownQty { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("createuser_id")]
        public int CreateUserId { get; set; }
        [Column("threshold_type_id")]
        public int ThresholdTypeId { get; set; }
    }
}
