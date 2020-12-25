using domain.user;
using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;
using irespository.user.model;
using iservice.user;
using System.Collections.Generic;

namespace service.user
{
    public class UserService: IUserService
    {
        private readonly UserContext _userContext;
        public UserService(UserContext userContext)
        {
            _userContext = userContext;
        }

        public PagerResult<UserListApiModel> GetPagerList(PagerQuery<UserListQueryModel> query)
        {
            return _userContext.GetPagerList(query);
        }

        public IEnumerable<UserRoleListApiModel> GetUserRoleList(int userId)
        {
            return _userContext.GetUserRoleList(userId);
        }

        public int UpdateUserRoleList(UserRoleListUpdateModel updated)
        {
            return _userContext.UpdateUserRoleList(updated);
        }

        public User UpdateIsActive(int userId, bool isActive)
        {
            return _userContext.UpdateIsActive(userId, isActive);
        }
    }
}
