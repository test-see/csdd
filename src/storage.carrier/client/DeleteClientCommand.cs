using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class DeleteClientCommand :ICommand
    {
        public int Id { get; set; }
    }
}
