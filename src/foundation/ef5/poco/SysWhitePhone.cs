﻿using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("sys_white_phone")]
    public class SysWhitePhone
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("phone")]
        public string Phone { get; set; }
    }
}