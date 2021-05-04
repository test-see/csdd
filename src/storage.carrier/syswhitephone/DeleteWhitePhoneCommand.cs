using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class DeleteWhitePhoneCommand : ICommand
    {
        public int Id { get; set; }
    }
}
