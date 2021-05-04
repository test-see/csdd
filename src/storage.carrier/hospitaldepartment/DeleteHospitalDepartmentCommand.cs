using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class DeleteHospitalDepartmentCommand:ICommand
    {
        public int Id { get; set; }
    }
}
