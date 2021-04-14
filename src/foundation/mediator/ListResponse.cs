using Mediator.Net.Contracts;

namespace foundation.mediator
{
    public class ListResponse<T> : IResponse
    {
        public T[] Payloads { get; set; }
        public ListResponse(params T[] payloads)
        {
            Payloads = payloads;
        }
    }
}
