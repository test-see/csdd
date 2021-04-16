using Mediator.Net.Contracts;

namespace irespository.sys.model
{
    public class ListEventlogByGoodsIdRequest:IRequest
    {
        public int[] GoodsIds { get; set; }
    }
}
