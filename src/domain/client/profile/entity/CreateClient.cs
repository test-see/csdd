using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class CreateClient : IRequest
    {
        public string Name { get; set; }
        public int UserId { get; set; }
    }
}
