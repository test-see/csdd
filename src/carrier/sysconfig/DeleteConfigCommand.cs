using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class DeleteConfigCommand : ICommand
    {
        public int Id { get; set; }
    }
}
