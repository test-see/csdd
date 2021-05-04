using Mediator.Net.Contracts;

namespace domain.client.goods2hospitalgoods.entity
{
    public class DeleteClientGoods2HospitalGoodsCommand:ICommand
    {
        public int Id { get; set; }
    }
}
