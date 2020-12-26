using System.Collections.Generic;

namespace irespository.sys.model
{
    public class RoleCreateApiModel
    {
        public string RoleName { get; set; }

        public IList<int> MenuIds { get; set; }
    }
}
