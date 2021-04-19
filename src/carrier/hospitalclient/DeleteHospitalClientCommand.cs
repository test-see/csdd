using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class DeleteHospitalClientCommand:ICommand
    {
        public int Id { get; set; }
    }
}
