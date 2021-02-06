using System.Collections.Generic;

namespace irespository.client.goods.model
{
    public class ClientGoodsUpdateApiModel
    {
        public string Name { get; set; }
        public string Spec { get; set; }
        public string Unit { get; set; }
        public string Producer { get; set; }
        public int IsActive { get; set; }
    }
}
