using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class DeleteHospitalClient:ICommand
    {
        public int Id { get; set; }
    }
}
