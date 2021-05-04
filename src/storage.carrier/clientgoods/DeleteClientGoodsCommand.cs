using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class DeleteClientGoodsCommand:ICommand
    {
        public int Id { get; set; }
    }
}
