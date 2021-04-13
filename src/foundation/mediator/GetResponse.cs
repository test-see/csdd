using Mediator.Net.Contracts;

namespace foundation.mediator
{
    public class GetResponse<T> : IResponse
    {
        public T[] Payloads { get; set; }
        public GetResponse(params T[] payloads)
        {
            Payloads = payloads;
        }
    }
}
