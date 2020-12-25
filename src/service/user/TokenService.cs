using domain.user;
using foundation.ef5.poco;
using irespository.user;
using iservice.user;
using System.Threading.Tasks;

namespace service.user
{
    public class TokenService: ITokenService
    {
        private readonly TokenContext _tokenContext;
        public TokenService(TokenContext tokenContext)
        {
            _tokenContext = tokenContext;
        }
        public async Task<string> GenerateVerificationCodeAsync(string phone)
        {
            var code = await _tokenContext.GenerateVerificationCodeAsync(phone);
            return code;
        }
        public User Login(LoginApiModel login)
        {
            return _tokenContext.Login(login);
        }
    }
}
