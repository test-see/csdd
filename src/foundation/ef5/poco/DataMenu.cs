﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("data_menu")]
    public class DataMenu
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("path")]
        public string Path { get; set; }
        [Column("patient_id")]
        public int PatientId { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
    }
}
