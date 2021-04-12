using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class DeletingClient : ICommand
    {
        public int Id { get; set; }
    }
}
