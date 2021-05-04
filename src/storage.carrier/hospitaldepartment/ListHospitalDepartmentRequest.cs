using Mediator.Net.Contracts;

namespace irespository.hospital.department.model
{
    public class ListHospitalDepartmentRequest : IRequest
    {
        public int? HospitalId { get; set; }
        public string Name { get; set; }
        public int? DepartmentTypeId { get; set; }
    }
}
