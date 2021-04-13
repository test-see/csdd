using Mediator.Net.Contracts;

namespace foundation.mediator
{
    public class StorageRequest<T> : IRequest
    {
        public T Payload { get; set; }
        public StorageRequest(T payload)
        {
            Payload = payload;
        }
    }
}
