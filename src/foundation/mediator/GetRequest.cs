using Mediator.Net.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace foundation.mediator
{
    public class GetRequest:IRequest
    {
        public GetRequest(params int[] ids)
        {
            Ids = ids;
        }
        public int[] Ids { get; set; }
        public GetRequest(IList<int> ids)
        {
            Ids = ids.ToArray();
        }
    }
}
