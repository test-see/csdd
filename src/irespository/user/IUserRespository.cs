using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;
using irespository.user.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace irespository.user
{
    public interface IUserRespository
    {
        Task<User> AddActiveUserAsync(UserCreateApiModel created, int userId);
        User GetByPhone(string phone);
        PagerResult<UserListApiModel> GetPagerList(PagerQuery<UserListQueryModel> query);
        User UpdateIsActive(int userId, bool isActive);
        int Update(int id, UserUpdateApiModel updated);
        UserIndexApiModel GetIndex(int userId);
    }
}
