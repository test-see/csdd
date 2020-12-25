using foundation.config;
using foundation.ef5.poco;
using irespository.user.model;
using System.Threading.Tasks;

namespace irespository.user
{
    public interface IUserRespository
    {
        Task AddActiveUserAsync(string phone);
        User GetByPhone(string phone);
        PagerResult<User> GetPagerList(PagerQuery<UserListQueryModel> query);
        User UpdateIsActive(int userId, bool isActive);
    }
}
