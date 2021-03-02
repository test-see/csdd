using domain.user.valuemodel;
using foundation.ef5.poco;
using irespository.user;
using irespository.user.enums;
using irespository.user.hospital.model;
using System.Threading.Tasks;

namespace iservice.user
{
    public interface ITokenService
    {
        User Login(LoginApiModel login);
        Task<string> GenerateVerificationCodeAsync(string phone);
        LoginHospitalValueModel LoginByHospital(LoginApiModel login);
        LoginClientValueModel LoginByClient(LoginApiModel login);
    }
}
