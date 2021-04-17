using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class DeleteHospitalDepartment:ICommand
    {
        public int Id { get; set; }
    }
}
