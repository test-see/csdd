using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;
using irespository.user;
using irespository.user.model;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public PagerResult<UserListApiModel> GetPagerList(PagerQuery<UserListQueryModel> query)
        {
            return _userRespository.GetPagerList(query);
        }
        public User UpdateIsActive(int userId, bool isActive)
        {
            return _userRespository.UpdateIsActive(userId, isActive);
        }
        public IEnumerable<UserRoleListApiModel> GetUserRoleList(int userId)
        {
            return _userRoleRespository.GetUserRoleList(userId);
        }

        public int UpdateUser(UserUpdateApiModel updated)
        {
            return _userRoleRespository.UpdateUser(updated);
        }
        public async Task<User> AddActiveUserAsync(UserCreateApiModel created, int userId)
        {
            return await _userRespository.AddActiveUserAsync(created, userId);
        }
    }
}
