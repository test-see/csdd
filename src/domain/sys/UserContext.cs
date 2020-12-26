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
        public UserContext(IUserRespository userRespository)
        {
            _userRespository = userRespository;
        }
        public PagerResult<UserListApiModel> GetPagerList(PagerQuery<UserListQueryModel> query)
        {
            return _userRespository.GetPagerList(query);
        }
        public User UpdateIsActive(int userId, bool isActive)
        {
            return _userRespository.UpdateIsActive(userId, isActive);
        }
        public UserIndexApiModel GetUserIndex(int userId)
        {
            return _userRespository.GetUserIndex(userId);
        }

        public int UpdateUser(UserUpdateApiModel updated)
        {
            return _userRespository.UpdateUser(updated);
        }
        public async Task<User> AddActiveUserAsync(UserCreateApiModel created, int userId)
        {
            return await _userRespository.AddActiveUserAsync(created, userId);
        }
    }
}
