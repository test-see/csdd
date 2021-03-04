using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;
using irespository.user.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iservice.user
{
    public interface IUserService
    {
        PagerResult<UserListApiModel> GetPagerList(PagerQuery<UserListQueryModel> query);
        UserIndexApiModel GetIndex(int userId);
        int Update(int id, UserUpdateApiModel updated);
        User UpdateIsActive(int userId, bool isActive);
        Task<User> CreateAsync(UserCreateApiModel created, int userId);
        IEnumerable<DataPortal> GetAuthorizeList();
    }
}
