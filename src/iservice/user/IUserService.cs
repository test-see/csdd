using foundation.config;
using foundation.ef5.poco;
using irespository.user;
using irespository.user.model;
using System.Threading.Tasks;

namespace iservice.user
{
    public interface IUserService
    {
        User Login(LoginApiModel login);
        Task<string> GenerateVerificationCodeAsync(string phone);
        PagerResult<User> GetPagerList(PagerQuery<UserListQueryModel> query);
    }
}
