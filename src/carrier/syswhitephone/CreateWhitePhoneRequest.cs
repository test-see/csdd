using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class CreateWhitePhoneRequest : IRequest
    {
        public string Phone { get; set; }
        public int UserId { get; set; }
    }
}
