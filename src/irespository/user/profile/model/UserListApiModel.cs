using foundation.config;
using foundation.ef5.poco;
using System;
using System.Collections.Generic;

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
        public DataAuthorizeRole AuthorizeRole { get; set; }
        public IList<IdNameValueModel> Roles { get; set; }
    }
}
