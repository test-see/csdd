using Mediator.Net.Contracts;

namespace foundation.mediator
{
    public class PipeRequest<T> :IRequest
    {
        public T Payload { get; set; }
        public PipeRequest(T payload)
        {
            Payload = payload;
        }
    }
}
