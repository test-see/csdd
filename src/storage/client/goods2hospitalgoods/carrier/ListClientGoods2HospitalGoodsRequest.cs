using Mediator.Net.Contracts;

namespace storage.client.goods2hospitalgoods.carrier
{
    public class ListClientGoods2HospitalGoodsRequest:IRequest
    {
        public int[] ClientGoodsIds { get; set; }
    }
}
