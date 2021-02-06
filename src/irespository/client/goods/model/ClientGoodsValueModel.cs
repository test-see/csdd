using irespository.client.profile.model;

namespace irespository.client.goods.model
{
    public class ClientGoodsValueModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Spec { get; set; }
        public string Unit { get; set; }
        public string Producer { get; set; }
        public ClientValueModel Client { get; set; }
    }
}
