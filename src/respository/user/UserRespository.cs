using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.user;
using irespository.user.model;
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
        public async Task AddActiveUserAsync(string phone, int userId)
        {
            _context.User.Add(new User
            {
                IsActive = 1,
                Username = phone,
                Phone = phone,
                CreateTime = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();
        }
        public User GetByPhone(string phone)
        {
            return _context.User.Where(x => x.Phone == phone).FirstOrDefault();
        }
        public PagerResult<UserListApiModel> GetPagerList(PagerQuery<UserListQueryModel> query)
        {
            var sql = from r in _context.User
                      join p in _context.User on r.CreateUserId equals p.Id into p_t
                      from p_tt in p_t.DefaultIfEmpty()
                      select new UserListApiModel
                      {
                          Id = r.Id,
                          IsActive = r.IsActive,
                          Phone = r.Phone,
                          Username = r.Username,
                          CreateTime = r.CreateTime,
                          CreateUsername = p_tt.Username,
                      };
            return new PagerResult<UserListApiModel>(query.Index, query.Size, sql);
        }

        public User UpdateIsActive(int userId, bool isActive)
        {
            var user = _context.User.Where(x => x.Id == userId).FirstOrDefault();
            user.IsActive = isActive ? 1 : 0;
            _context.User.Update(user);
            _context.SaveChanges();
            return user;
        }

    }
}
