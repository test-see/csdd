using domain.user;
using domain.user.valuemodel;
using foundation.ef5.poco;
using irespository.user;
using iservice.user;
using System.Threading.Tasks;

namespace service.user
{
    public class TokenService : ITokenService
    {
        private readonly TokenContext _tokenContext;
        private readonly UserHospitalContext _userHospitalContext;
        public TokenService(TokenContext tokenContext,
            UserHospitalContext userHospitalContext)
        {
            _tokenContext = tokenContext;
            _userHospitalContext = userHospitalContext;
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

        public LoginHospitalValueModel LoginByHospital(LoginApiModel login)
        {
            var user = _tokenContext.Login(login);
            var extend = _userHospitalContext.GetIndexByUserId(user.Id);
            return new LoginHospitalValueModel
            {
                AuthorizeRoleId = user.AuthorizeRoleId,
                HospitalDepartmentId = extend.HospitalDepartment.Id,
                HospitalId = extend.HospitalDepartment.Hospital.Id,
                Id = user.Id,
            };
        }
    }
}
