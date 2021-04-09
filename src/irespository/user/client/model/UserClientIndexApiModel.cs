using irespository.user.profile.model;
using nouns.client.profile;
using System;

namespace irespository.user.client.model
{
    public class UserClientIndexApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GetClientResponse Client { get; set; }
        public UserValueModel User { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
    }
}
