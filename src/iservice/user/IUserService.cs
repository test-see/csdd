using foundation.ef5.poco;
using irespository.user;
using System.Threading.Tasks;

namespace iservice.user
{
    public interface IUserService
    {
        User LoginOrRegister(LoginOrRegisterApiModel login);
        Task<string> GenerateVerificationCodeAsync(string phone);
    }
}
