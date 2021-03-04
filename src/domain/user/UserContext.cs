using foundation.config;
using foundation.ef5.poco;
using foundation.exception;
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
        private readonly IPortalRespository _authorizeRoleRespository;
        public UserContext(IUserRespository userRespository, 
            IPortalRespository authorizeRoleRespository)
        {
            _userRespository = userRespository;
            _authorizeRoleRespository = authorizeRoleRespository;
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
        public async Task<User> CreateAsync(UserCreateApiModel created, int userId)
        {
            var user = _userRespository.GetByPhone(created.Phone);
            if (user != null)
                throw new DefaultException("电话号码已经被占用.");
            return await _userRespository.CreateAsync(created, userId);
        }
        public IEnumerable<DataPortal> GetAuthorizeList()
        {
            return _authorizeRoleRespository.GetList();
        }
    }
}
