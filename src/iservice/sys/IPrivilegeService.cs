﻿using irespository.sys.model;
using System.Collections.Generic;

namespace iservice.sys
{
    public interface IPrivilegeService
    {
        IEnumerable<PrivilegeListApiModel> GetPrivilegeList(int roleId);
    }
}
