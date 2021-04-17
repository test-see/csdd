using Mediator.Net.Contracts;

namespace irespository.hospital.model
{
    public class CreateHospitalClient:IRequest
    {
        public string Name { get; set; }
        public int HospitalId { get; set; }
        public int UserId { get; set; }
    }
}
