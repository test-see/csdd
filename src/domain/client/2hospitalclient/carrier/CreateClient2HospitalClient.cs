using Mediator.Net.Contracts;

namespace irespository.client.maping.model
{
    public class CreateClient2HospitalClient:IRequest
    {
        public int ClientId { get; set; }
        public int HospitalClientId { get; set; }
        public int UserId { get; set; }
    }
}
