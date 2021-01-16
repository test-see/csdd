using domain.user;
using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;
using irespository.user.model;
using iservice.user;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public UserIndexApiModel GetIndex(int userId)
        {
            return _userContext.GetIndex(userId);
        }

        public int Update(int id, UserUpdateApiModel updated)
        {
            return _userContext.Update(id, updated);
        }

        public User UpdateIsActive(int userId, bool isActive)
        {
            return _userContext.UpdateIsActive(userId, isActive);
        }
        public async Task<User> AddAsync(UserCreateApiModel created, int userId)
        {
            return await _userContext.AddAsync(created, userId);
        }
        public IEnumerable<DataAuthorizeRole> GetDataAuthorizeList()
        {
            return _userContext.GetDataAuthorizeList();
        }
    }
}
