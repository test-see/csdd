using System.Collections.Generic;

namespace irespository.sys.model
{
    public class RoleIndexApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<int> MenuIds { get; set; }
    }
}
