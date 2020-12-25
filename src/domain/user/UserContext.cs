using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;
using irespository.user;
using irespository.user.model;
using System.Collections.Generic;

namespace domain.user
{
    public class UserContext
    {
        private readonly IUserRespository _userRespository;
        private readonly IUserRoleRespository _userRoleRespository;
        public UserContext(IUserRespository userRespository,
            IUserRoleRespository userRoleRespository)
        {
            _userRespository = userRespository;
            _userRoleRespository = userRoleRespository;
        }
        public PagerResult<User> GetPagerList(PagerQuery<UserListQueryModel> query)
        {
            return _userRespository.GetPagerList(query);
        }
        public IEnumerable<UserRoleListApiModel> GetUserRoleList(int userId)
        {
            return _userRoleRespository.GetUserRoleList(userId);
        }

        public int UpdateUserRoleList(UserRoleListUpdateModel updated)
        {
            return _userRoleRespository.UpdateUserRoleList(updated);
        }
    }
}
