using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("data_menu")]
    public class DataMenu
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("authorize_role_id")]
        public int AuthorizeRoleId { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("path")]
        public string Path { get; set; }
        [Column("parent_id")]
        public int ParentId { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("rank")]
        public int Rank { get; set; }
    }
}
