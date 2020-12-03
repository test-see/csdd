using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("tourist_hospital_preference")]
    public class TouristHospitalPreference
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("tourist_id")]
        public int TouristId { get; set; }
        [Column("department_id")]
        public int DepartmentId { get; set; }
    }
}
