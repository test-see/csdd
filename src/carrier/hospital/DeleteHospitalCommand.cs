using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class DeleteHospitalCommand : ICommand
    {
        public int Id { get; set; }
    }
}
