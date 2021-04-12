using Mediator.Net.Contracts;

namespace domain.client.entity
{
    public class CreateClientEntity : IRequest
    {
        public string Name { get; set; }
        public int UserId { get; set; }
    }
}
