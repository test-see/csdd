using irespository.user;
using System.Threading.Tasks;
using System;
using foundation.ef5.poco;

namespace domain.user
{
    public class UserLoginContext
    {
        private readonly IUserRespository _userRespository;
        private readonly IUserVerificationCodeRespository _userVerificationCodeRespository;
        public UserLoginContext(IUserRespository userRespository,
            IUserVerificationCodeRespository userVerificationCodeRespository)
        {
            _userRespository = userRespository;
            _userVerificationCodeRespository = userVerificationCodeRespository;
        }
        public async Task<User> LoginOrRegisterAsync(LoginOrRegisterApiModel login)
        {
            if (!_userVerificationCodeRespository.CheckVerificationCode(login))
                throw new UnauthorizedAccessException();
            var user = _userRespository.GetByPhone(login.Phone);
            if (user == null)
            {
                // await _userRespository.AddActiveUserAsync(login.Phone);
                throw new UnauthorizedAccessException();
            }
            return user;
        }
        public async Task<string> GenerateVerificationCodeAsync(string phone)
        {
            // check white phone list
            if (_userVerificationCodeRespository.GetCountVerificationCodeInMinuteOne(phone) > 0)
                throw new InvalidOperationException("get the verification code too more.");
            await _userVerificationCodeRespository.InActiveVerificationCodeListAsync(phone);
            return await _userVerificationCodeRespository.AddVerificationCodeAsync(phone);
        }
    }
}
