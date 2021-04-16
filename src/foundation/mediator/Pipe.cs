using Mediator.Net.Contracts;

namespace foundation.mediator
{
    public class Pipe<T> :IRequest, ICommand
    {
        public T Payload { get; set; }
        public Pipe(T payload)
        {
            Payload = payload;
        }
    }
}
