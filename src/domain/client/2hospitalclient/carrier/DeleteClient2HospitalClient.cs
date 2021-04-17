using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class DeleteClient2HospitalClient:ICommand
    {
        public int Id { get; set; }
    }
}
