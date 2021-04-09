using nouns.client.profile;

namespace irespository.client.goods.model
{
    public class ClientGoodsValueModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Spec { get; set; }
        public string Unit { get; set; }
        public string Producer { get; set; }
        public GetClientResponse Client { get; set; }
    }
}
