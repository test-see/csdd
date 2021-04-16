using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class DeleteHospitalGoods : ICommand
    {
        public int Id { get; set; }
    }
}
