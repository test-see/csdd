using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class DeleteHospital : ICommand
    {
        public int Id { get; set; }
    }
}
