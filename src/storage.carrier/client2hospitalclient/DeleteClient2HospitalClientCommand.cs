using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class DeleteClient2HospitalClientCommand:ICommand
    {
        public int Id { get; set; }
    }
}
