using foundation.ef5;
using foundation.ef5.poco;
using irespository.user;
using System.Linq;

namespace respository.user
{
    public class SysWhitePhoneRespository : ISysWhitePhoneRespository
    {
        private readonly DefaultDbContext _context;
        public SysWhitePhoneRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public bool Exists(string phone)
        {
            return _context.SysWhitePhone.Where(x => x.Phone == phone).Any();
        }
    }
}
