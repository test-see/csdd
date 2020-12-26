using domain.user;
using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;
using irespository.user.model;
using iservice.user;
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

        public UserIndexApiModel GetUserIndex(int userId)
        {
            return _userContext.GetUserIndex(userId);
        }

        public int UpdateUser(UserUpdateApiModel updated)
        {
            return _userContext.UpdateUser(updated);
        }

        public User UpdateIsActive(int userId, bool isActive)
        {
            return _userContext.UpdateIsActive(userId, isActive);
        }
        public async Task<User> AddActiveUserAsync(UserCreateApiModel created, int userId)
        {
            return await _userContext.AddActiveUserAsync(created, userId);
        }
    }
}
