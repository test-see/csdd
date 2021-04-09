using Mediator.Net.Contracts;

namespace nouns.client.profile
{
    public class GetClientRequest : IRequest
    {
        public GetClientRequest(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
