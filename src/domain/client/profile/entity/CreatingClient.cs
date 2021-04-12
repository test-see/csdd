using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class CreatingClient : IRequest
    {
        public string Name { get; set; }
        public int UserId { get; set; }
    }
}
