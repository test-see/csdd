using foundation.ef5;
using foundation.ef5.poco;
using irespository.user;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace respository.user
{
    public class UserRespository : IUserRespository
    {
        private readonly DefaultDbContext _context;
        public UserRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task AddActiveUserAsync(string phone)
        {
            _context.User.Add(new User { IsActive = 1, Phone = phone, CreateTime = DateTime.UtcNow });
            await _context.SaveChangesAsync();
        }
        public User GetByPhone(string phone)
        {
            return _context.User.Where(x => x.Phone == phone && x.IsActive == 1).FirstOrDefault();
        }
    }
}
