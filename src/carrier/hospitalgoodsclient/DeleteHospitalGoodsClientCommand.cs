using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class DeleteHospitalGoodsClientCommand : ICommand
    {
        public int Id { get; set; }
    }
}
