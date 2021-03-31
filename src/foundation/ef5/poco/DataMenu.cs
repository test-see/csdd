using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("data_menu")]
    public class DataMenu
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("portal_id")]
        public int PortalId { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("displayname")]
        public string DisplayName { get; set; }
        [Column("path")]
        public string Path { get; set; }
        [Column("parent_id")]
        public int ParentId { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("rank")]
        public int Rank { get; set; }
        [Column("icon")]
        public string Icon { get; set; }
        [Column("is_hide")]
        public int IsHide { get; set; }
        [Column("is_hidechildren")]
        public int IsHideChildren { get; set; }
        [Column("is_parent")]
        public int IsParent { get; set; }
    }
}
