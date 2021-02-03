namespace irespository.hospital.department.model
{
    public class HospitalDepartmentUpdateApiModel
    {
        public int DepartmentTypeId { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public int IsCheck { get; set; }
    }
}
