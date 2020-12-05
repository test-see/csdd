using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("hospital_client")]
    public class HospitalClient
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("hospital_id")]
        public int HospitalId { get; set; }
    }
}
