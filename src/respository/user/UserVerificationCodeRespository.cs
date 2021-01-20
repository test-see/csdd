using foundation.ef5;
using foundation.ef5.poco;
using irespository.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace respository.user
{
    public class UserVerificationCodeRespository : IUserVerificationCodeRespository
    {
        private readonly DefaultDbContext _context;
        public UserVerificationCodeRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public bool CheckVerificationCode(LoginApiModel login)
        {
            var verification = _context.UserVerificationCode.Where(x => x.Expiration > DateTime.Now && x.IsActive == 1
                && x.VerificationCode == login.Code && x.Phone == login.Phone).FirstOrDefault();
            return verification != null;
        }
        public IEnumerable<UserVerificationCode> GetActiveVerificationCodeList(string phone)
        {
            return _context.UserVerificationCode.Where(x => x.IsActive == 1 && x.Phone == phone).ToList();
        }
        public int GetCountVerificationCodeInMinuteOne(string phone, bool isTest = false)
        {
            var limittime = isTest ? DateTime.Now.AddMinutes(-1) : DateTime.Now;
            var count = _context.UserVerificationCode.Where(x => x.CreateTime > limittime && x.IsActive == 1 && x.Phone == phone).Count();
            return count;
        }
        public async Task InActiveVerificationCodeListAsync(string phone)
        {
            var verifications = _context.UserVerificationCode.Where(x => x.IsActive == 1 && x.Phone == phone).ToList();
            foreach (var verification in verifications)
            {
                verification.IsActive = 0;
            }
            await _context.SaveChangesAsync();
        }
        public async Task<string> AddVerificationCodeAsync(string phone)
        {
            var randomcode = new Random().Next(0, 999999).ToString("000000");
            _context.UserVerificationCode.Add(new UserVerificationCode
            {
                IsActive = 1,
                Phone = phone,
                CreateTime = DateTime.Now,
                VerificationCode = randomcode,
                Expiration = DateTime.Now.AddMinutes(5),
            });
            await _context.SaveChangesAsync();
            return randomcode;
        }
    }
}
