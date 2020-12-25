using irespository.sys.model;
using System.Collections.Generic;

namespace irespository.user
{
    public interface IUserRoleRespository
    {
        IEnumerable<UserRoleListApiModel> GetUserRoleList(int userId);
        int UpdateUserRoleList(UserRoleListUpdateModel updated);
    }
}
