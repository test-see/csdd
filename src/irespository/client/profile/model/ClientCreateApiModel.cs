using System.Collections.Generic;

namespace irespository.client.model
{
    public class ClientCreateApiModel
    {
        public string Name { get; set; }
        public IList<int> HospitalClientIds { get; set; }
    }
}
