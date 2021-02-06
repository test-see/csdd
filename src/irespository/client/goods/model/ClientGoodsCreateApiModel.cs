using System.Collections.Generic;

namespace irespository.client.goods.model
{
    public class ClientGoodsCreateApiModel
    {
        public string Name { get; set; }
        public int ClientId { get; set; }
        public string Spec { get; set; }
        public string Unit { get; set; }
        public string Producer { get; set; }

}
}
