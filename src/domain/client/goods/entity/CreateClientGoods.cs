using System.Collections.Generic;

namespace irespository.client.goods.model
{
    public class CreateClientGoods
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
        public string Spec { get; set; }
        public string Unit { get; set; }
        public string Producer { get; set; }
        public int UserId { get; set; }

    }
}
