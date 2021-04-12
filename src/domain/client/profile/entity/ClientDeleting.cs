using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class ClientDeleting : ICommand
    {
        public int Id { get; set; }
    }
}
