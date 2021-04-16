using System.Collections.Generic;

namespace irespository.sys.model
{
    public class ListEventlogByGoodsIdResponse
    {
        public int GoodsId { get; set; }
        public ListEventlogResponse Log { get; set; }
    }
}
