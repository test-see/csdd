using System.Collections.Generic;

namespace irespository.user.model
{
    public class UserCreateApiModel
    {
        public string Phone { get; set; }
        public string Username { get; set; }
        //public 
        public IList<int> RoleIds { get; set; }
    }
}
