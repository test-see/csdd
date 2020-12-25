using foundation.ef5.poco;
using irespository.user;
using System.Threading.Tasks;

namespace iservice.user
{
    public interface ITokenService
    {
        User Login(LoginApiModel login);
        Task<string> GenerateVerificationCodeAsync(string phone);
    }
}
