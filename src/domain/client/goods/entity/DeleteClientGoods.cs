using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class DeleteClientGoods:ICommand
    {
        public int Id { get; set; }
    }
}
