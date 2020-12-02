﻿using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("client")]
    public class Client
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("province_id")]
        public int ProvinceId { get; set; }
    }
}
