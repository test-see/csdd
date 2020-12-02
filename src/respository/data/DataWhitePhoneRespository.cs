using foundation.ef5;
using foundation.ef5.poco;
using irespository.user;
using System.Linq;

namespace respository.user
{
    public class DataWhitePhoneRespository : IDataWhitePhoneRespository
    {
        private readonly DefaultDbContext _context;
        public DataWhitePhoneRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public bool Exists(string phone)
        {
            return _context.DataWhitePhone.Where(x => x.Phone == phone).Any();
        }
    }
}
