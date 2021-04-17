using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class DeleteClient :ICommand
    {
        public int Id { get; set; }
    }
}
