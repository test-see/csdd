using Mediator.Net.Contracts;

namespace foundation.mediator
{
    public class PipeCommand<T> :ICommand
    {
        public T Payload { get; set; }
        public PipeCommand(T payload)
        {
            Payload = payload;
        }
    }
}
