using foundation.ef5.poco;
using irespository.hospital.profile.model;

namespace irespository.hospital.department.model
{
    public class GetHospitalDepartmentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GetHospitalResponse Hospital { get; set; }
        public DataDepartmentType DepartmentType { get; set; }
    }
}
