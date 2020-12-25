﻿using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;
using irespository.user.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iservice.user
{
    public interface IUserService
    {
        PagerResult<UserListApiModel> GetPagerList(PagerQuery<UserListQueryModel> query);
        IEnumerable<UserRoleListApiModel> GetUserRoleList(int userId);
        int UpdateUserRoleList(UserRoleListUpdateModel updated);
        User UpdateIsActive(int userId, bool isActive);
        Task<User> AddActiveUserAsync(UserCreateApiModel created, int userId);
    }
}
