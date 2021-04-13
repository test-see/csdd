namespace irespository.client.goods.model
{
    public class GetClientGoodsRequest
    {
        public GetClientGoodsRequest(params int[] ids)
        {
            Ids = ids;
        }
        public int[] Ids { get; set; }
    }
}
