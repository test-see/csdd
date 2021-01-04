using irespository.user.enums;
using System.Collections.Generic;

namespace irespository.sys.model
{
    public class UserUpdateApiModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int AuthorizeRoleId { get; set; }
        public IList<int> RoleIds { get; set; }
    }
}
