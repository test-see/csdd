using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("data_department_type")]
    public class DataDepartmentType
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
    }
}
