using System;

namespace irespository.user.model
{
    public class UserListApiModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public DateTime CreateTime { get; set; }
        public int IsActive { get; set; }
        public string CreateUsername { get; set; }
        public string AuthorizeRoleName { get; set; }
    }
}
