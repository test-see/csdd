using foundation.config;
using foundation.ef5.poco;
using irespository.data;
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
        public UserIndexApiModel GetIndex(int userId)
        {
            return _userRespository.GetIndex(userId);
        }

        public int Update(int id, UserUpdateApiModel updated)
        {
            return _userRespository.Update(id, updated);
        }
        public async Task<User> AddAsync(UserCreateApiModel created, int userId)
        {
            return await _userRespository.AddAsync(created, userId);
        }
    }
}
