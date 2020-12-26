using System.Collections.Generic;

namespace irespository.sys.model
{
    public class UserUpdateApiModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public IList<int> RoleIds { get; set; }
    }
}
