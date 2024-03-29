﻿using foundation.config;
using System.Collections.Generic;

namespace irespository.sys.model
{
    public class UserIndexApiModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public IList<IdNameValueModel> Roles { get; set; }
    }
}
