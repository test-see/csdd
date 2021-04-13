using Mediator.Net.Contracts;

namespace foundation.mediator
{
    public class StorageCommand<T> : ICommand
    {
        public T Payload { get; set; }
        public StorageCommand(T payload)
        {
            Payload = payload;
        }
    }
}
