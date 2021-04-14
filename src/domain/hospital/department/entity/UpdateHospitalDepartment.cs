namespace irespository.hospital.model
{
    public class UpdateHospitalDepartment
    {
        public int DepartmentTypeId { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public int IsPurchaseCheck { get; set; }
        public int Id { get; set; }
    }
}
