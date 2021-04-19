using Mediator.Net.Contracts;
using System.Collections.Generic;

namespace irespository.client.goods.model
{
    public class CreateClientGoodsRequest:IRequest
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
