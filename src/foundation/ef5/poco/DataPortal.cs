using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("data_portal")]
    public class DataPortal
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
    }
}
