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
        private readonly IDataWhitePhoneRespository _dataWhitePhoneRespository;
        public UserLoginContext(IUserRespository userRespository,
            IUserVerificationCodeRespository userVerificationCodeRespository,
            IDataWhitePhoneRespository dataWhitePhoneRespository)
        {
            _userRespository = userRespository;
            _userVerificationCodeRespository = userVerificationCodeRespository;
            _dataWhitePhoneRespository = dataWhitePhoneRespository;
        }
        public User LoginOrRegister(LoginOrRegisterApiModel login)
        {
            if (!_userVerificationCodeRespository.CheckVerificationCode(login))
                throw new UnauthorizedAccessException("phone or code is invalid.");
            var user = _userRespository.GetByPhone(login.Phone);
            if (user == null)
                throw new UnauthorizedAccessException("the user isnot exist.");
            return user;
        }
        public async Task<string> GenerateVerificationCodeAsync(string phone)
        {
            var whitephone = _dataWhitePhoneRespository.GetByPhone(phone);
            if (whitephone == null)
                throw new UnauthorizedAccessException("this phone is unauth.");
            if (_userVerificationCodeRespository.GetCountVerificationCodeInMinuteOne(phone) > 0)
                throw new InvalidOperationException("get the verification code too more.");
            await _userVerificationCodeRespository.InActiveVerificationCodeListAsync(phone);
            return await _userVerificationCodeRespository.AddVerificationCodeAsync(phone);
        }
    }
}
