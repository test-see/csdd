namespace irespository.hospital.department.model
{
    public class HospitalDepartmentCreateApiModel
    {
        public string Name { get; set; }
        public int HospitalId { get; set; }
        public int DepartmentTypeId { get; set; }
        public int ParentId { get; set; }
    }
}
