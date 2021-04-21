using foundation.ef5.poco;
using foundation.exception;
using foundation.mediator;
using irespository.sys.model;
using irespository.user;
using Mediator.Net;
using System.Threading.Tasks;

namespace domain.user
{
    public class TokenContext
    {
        private readonly IUserRespository _userRespository;
        private readonly IUserVerificationCodeRespository _userVerificationCodeRespository;
        private readonly IMediator _mediator;
        public TokenContext(IUserRespository userRespository,
            IUserVerificationCodeRespository userVerificationCodeRespository,
            IMediator mediator)
        {
            _userRespository = userRespository;
            _userVerificationCodeRespository = userVerificationCodeRespository;
            _mediator = mediator;
        }
        public User Login(LoginApiModel login)
        {
            if (!_userVerificationCodeRespository.CheckVerificationCode(login))
                throw new DefaultException("电话号码 或者 验证码不正确.");
            var user = _userRespository.GetByPhone(login.Phone);
            if (user == null)
                throw new DefaultException("用户不存在.");
            if (user.IsActive == 0)
                throw new DefaultException("该用户已被冻结, 请联系管理员激活.");
            return user;
        }
        public async Task<string> GenerateVerificationCodeAsync(string phone)
        {
            var white = await _mediator.GetAsync<ListWhitePhoneRequest, ListWhitePhoneResponse>(new ListWhitePhoneRequest { Phone = phone });
            if (white == null)
                throw new DefaultException("该电话未被授权使用.");

            if (_userVerificationCodeRespository.GetCountVerificationCodeInMinuteOne(phone) > 0)
                throw new DefaultException("您获取验证码的频率太高了,请稍后再获取.");
            await _userVerificationCodeRespository.InActiveVerificationCodeListAsync(phone);
            return await _userVerificationCodeRespository.AddVerificationCodeAsync(phone);
        }
    }
}
