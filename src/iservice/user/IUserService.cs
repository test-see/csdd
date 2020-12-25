using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;
using irespository.user.model;
using System.Collections.Generic;

namespace iservice.user
{
    public interface IUserService
    {
        PagerResult<User> GetPagerList(PagerQuery<UserListQueryModel> query);
        IEnumerable<UserRoleListApiModel> GetUserRoleList(int userId);
        int UpdateUserRoleList(UserRoleListUpdateModel updated);
    }
}
