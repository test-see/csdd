using foundation.ef5.poco;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace irespository.user
{
    public interface IUserVerificationCodeRespository
    {
        bool CheckVerificationCode(LoginApiModel login);
        IEnumerable<UserVerificationCode> GetActiveVerificationCodeList(string phone);
        int GetCountVerificationCodeInMinuteOne(string phone, bool isTest = false);
        Task InActiveVerificationCodeListAsync(string phone);
        Task<string> AddVerificationCodeAsync(string phone);
    }
}
