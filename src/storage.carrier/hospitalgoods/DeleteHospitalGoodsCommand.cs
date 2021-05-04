using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class DeleteHospitalGoodsCommand : ICommand
    {
        public int Id { get; set; }
    }
}
