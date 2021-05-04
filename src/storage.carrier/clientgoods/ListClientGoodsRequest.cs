namespace irespository.client.goods.model
{
    public class ListClientGoodsRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int? IsActive { get; set; }
        public int? ClientId { get; set; }
    }
}
