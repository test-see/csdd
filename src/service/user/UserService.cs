using domain.user;
using foundation.ef5.poco;
using irespository.user;
using iservice.user;
using System.Threading.Tasks;

namespace service.user
{
    public class UserService: IUserService
    {
        private readonly UserLoginContext _userLoginContext;
        public UserService(UserLoginContext userLoginContext)
        {
            _userLoginContext = userLoginContext;
        }
        public async Task<string> GenerateVerificationCodeAsync(string phone)
        {
            var code = await _userLoginContext.GenerateVerificationCodeAsync(phone);
            return code;
        }
        public User Login(LoginApiModel login)
        {
            return _userLoginContext.Login(login);
        }
    }
}
