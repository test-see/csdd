using foundation.config;
using foundation.ef5.poco;
using irespository.user.model;
using System.Threading.Tasks;

namespace irespository.user
{
    public interface IUserRespository
    {
        Task AddActiveUserAsync(string phone, int userId);
        User GetByPhone(string phone);
        PagerResult<UserListApiModel> GetPagerList(PagerQuery<UserListQueryModel> query);
        User UpdateIsActive(int userId, bool isActive);
    }
}
