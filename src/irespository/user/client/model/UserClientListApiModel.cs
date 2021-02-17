using irespository.client.profile.model;
using irespository.user.profile.model;
using System;

namespace irespository.user.client.model
{
    public class UserClientListApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ClientValueModel Client { get; set; }
        public UserValueModel User { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
    }
}
