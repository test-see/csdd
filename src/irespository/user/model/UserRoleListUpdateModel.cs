using System.Collections.Generic;

namespace irespository.sys.model
{
    public class UserRoleListUpdateModel
    {
        public int UserId { get; set; }
        public IList<int> RoleIds { get; set; }
    }
}
