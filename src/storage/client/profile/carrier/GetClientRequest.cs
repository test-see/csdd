using Mediator.Net.Contracts;

namespace nouns.client.profile
{
    public class GetClientRequest 
    {
        public GetClientRequest(params int[] ids)
        {
            Ids = ids;
        }
        public int[] Ids { get; set; }
    }
}
