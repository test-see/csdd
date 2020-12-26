using irespository.user.model;
using System.Collections.Generic;

namespace irespository.sys.model
{
    public class UserIndexApiModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public IList<UserRoleCheckApiModel> Roles { get; set; }
    }
}
