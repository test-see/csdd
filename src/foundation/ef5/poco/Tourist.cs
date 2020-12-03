using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("tourist")]
    public class Tourist
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("username")]
        public string Username { get; set; }
        [Column("phone")]
        public string Phone { get; set; }
        [Column("province_id")]
        public int ProvinceId { get; set; }
        [Column("identity_category_id")]
        public int IdentityCategoryId { get; set; }
        [Column("status")]
        public int Status { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("checktime")]
        public DateTime? CheckTime { get; set; }
    }
}
