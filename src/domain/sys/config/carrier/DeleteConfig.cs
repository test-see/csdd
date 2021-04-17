using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class DeleteConfig : ICommand
    {
        public int Id { get; set; }
    }
}
