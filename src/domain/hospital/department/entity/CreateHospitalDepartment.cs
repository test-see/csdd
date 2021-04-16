using Mediator.Net.Contracts;

namespace irespository.hospital.model
{
    public class CreateHospitalDepartment:IRequest
    {
        public string Name { get; set; }
        public int HospitalId { get; set; }
        public int DepartmentTypeId { get; set; }
        public int ParentId { get; set; }
        public int IsPurchaseCheck { get; set; }
        public int UserId { get; set; }
    }
}
