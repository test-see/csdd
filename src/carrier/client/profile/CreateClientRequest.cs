using Mediator.Net.Contracts;

namespace irespository.client.model
{
    public class CreateClientRequest: IRequest
    {
        public string Name { get; set; }
        public int UserId { get; set; }
    }
}
