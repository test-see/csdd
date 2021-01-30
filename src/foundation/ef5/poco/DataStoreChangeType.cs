using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("data_store_changetype")]
    public class DataStoreChangeType
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("operator")]
        public int Operator { get; set; }
        [Column("is_customize")]
        public int IsCustomize { get; set; }
    }
}
