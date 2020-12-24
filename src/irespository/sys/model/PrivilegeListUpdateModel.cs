using System.Collections.Generic;

namespace irespository.sys.model
{
    public class PrivilegeListUpdateModel
    {
        public int RoleId { get; set; }
        public IList<int> MenuIds { get; set; }
    }
}
