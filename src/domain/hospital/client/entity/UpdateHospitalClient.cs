using Mediator.Net.Contracts;

namespace irespository.hospital.model
{
    public class UpdateHospitalClient:IRequest
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
