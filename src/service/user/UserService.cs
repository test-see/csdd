using domain.user;
using foundation.config;
using foundation.ef5.poco;
using irespository.user;
using irespository.user.model;
using iservice.user;
using System.Threading.Tasks;

namespace service.user
{
    public class UserService: IUserService
    {
        private readonly TokenContext _tokenContext;
        private readonly UserContext _userContext;
        public UserService(TokenContext tokenContext,
            UserContext userContext)
        {
            _tokenContext = tokenContext;
            _userContext = userContext;
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

        public PagerResult<User> GetPagerList(PagerQuery<UserListQueryModel> query)
        {
            return _userContext.GetPagerList(query);
        }
    }
}
