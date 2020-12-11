using foundation.ef5.poco;
using foundation.exception;
using irespository.user;
using System.Threading.Tasks;

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
        public User Login(LoginApiModel login)
        {
            if (!_userVerificationCodeRespository.CheckVerificationCode(login))
                throw new DefaultException("phone or code is invalid.");
            var user = _userRespository.GetByPhone(login.Phone);
            if (user == null)
                throw new DefaultException("the user isnot exist.");
            if (user.IsActive == 0)
                throw new DefaultException("the user isnot active.");
            return user;
        }
        public async Task<string> GenerateVerificationCodeAsync(string phone)
        {
            if (!_dataWhitePhoneRespository.Exists(phone))
                throw new DefaultException("this phone is unauth.");
            if (_userVerificationCodeRespository.GetCountVerificationCodeInMinuteOne(phone) > 0)
                throw new DefaultException("get the verification code too more.");
            await _userVerificationCodeRespository.InActiveVerificationCodeListAsync(phone);
            return await _userVerificationCodeRespository.AddVerificationCodeAsync(phone);
        }
    }
}
