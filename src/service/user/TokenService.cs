using domain.user;
using domain.user.valuemodel;
using foundation._3party;
using foundation.ef5.poco;
using foundation.exception;
using irespository.user;
using irespository.user.enums;
using iservice.user;
using System.Threading.Tasks;

namespace service.user
{
    public class TokenService : ITokenService
    {
        private readonly TokenContext _tokenContext;
        private readonly UserHospitalContext _userHospitalContext;
        private readonly UserClientContext _userClientContext;
        private readonly SmsSendRequest _smsSendRequest;
        public TokenService(TokenContext tokenContext,
            UserHospitalContext userHospitalContext,
            UserClientContext userClientContext,
            SmsSendRequest smsSendRequest)
        {
            _tokenContext = tokenContext;
            _userHospitalContext = userHospitalContext;
            _userClientContext = userClientContext;
            _smsSendRequest = smsSendRequest;
        }
        public async Task<string> GenerateVerificationCodeAsync(string phone)
        {
            var code = await _tokenContext.GenerateVerificationCodeAsync(phone);
            await _smsSendRequest.SendVerificationCodeAsync(new string[] { phone }, code);
            return string.Empty;
        }
        public User Login(LoginApiModel login)
        {
            return _tokenContext.Login(login);
        }

        public async Task<LoginHospitalValueModel> LoginByHospitalAsync(LoginApiModel login)
        {
            var user = _tokenContext.Login(login);
            var extend = await _userHospitalContext.GetIndexByUserIdAsync(user.Id);
            if (extend == null)
                throw new DefaultException("该账号还在维护基础资料, 请等待.");
            return new LoginHospitalValueModel
            {
                HospitalDepartment = extend.HospitalDepartment,
                Id = user.Id,
                User = extend.User,
                Name = extend.Name,
            };
        }

        public async Task<LoginClientValueModel> LoginByClientAsync(LoginApiModel login)
        {
            var user = _tokenContext.Login(login);
            var extend = await _userClientContext.GetIndexByUserIdAsync(user.Id);
            if (extend == null)
                throw new DefaultException("该账号还在维护基础资料, 请等待.");
            return new LoginClientValueModel
            {
                Client = extend.Client,
                Id = user.Id,
                User = extend.User,
                Name = extend.Name,
            };
        }

    }
}
