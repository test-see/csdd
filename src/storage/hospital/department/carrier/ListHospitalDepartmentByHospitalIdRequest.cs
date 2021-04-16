using Mediator.Net.Contracts;

namespace storage.hospital.department.carrier
{
    public class ListHospitalDepartmentByHospitalIdRequest:IRequest
    {
        public int HospitalId { get; set; }
    }
}
