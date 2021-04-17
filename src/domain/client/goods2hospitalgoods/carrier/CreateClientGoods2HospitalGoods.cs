using Mediator.Net.Contracts;

namespace irespository.client.goods.model
{
    public class CreateClientGoods2HospitalGoods : IRequest
    {
        public int ClientGoodsId { get; set; }
        public int HospitalGoodsId { get; set; }
        public int ClientQty { get; set; }
        public int HospitalQty { get; set; }
        public int UserId { get; set; }
    }
}
