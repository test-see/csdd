using Mediator.Net.Contracts;

namespace irespository.client.model
{
    public class UpdateClientRequest : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }

    }
}
