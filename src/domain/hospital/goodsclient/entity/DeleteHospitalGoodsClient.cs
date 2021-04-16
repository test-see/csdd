using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class DeleteHospitalGoodsClient : ICommand
    {
        public int Id { get; set; }
    }
}
