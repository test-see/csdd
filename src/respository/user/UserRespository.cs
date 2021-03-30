using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.sys.model;
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
        public async Task<User> CreateAsync(UserCreateApiModel created, int userId)
        {
            var user = new User
            {
                IsActive = 1,
                Username = created.Username,
                Phone = created.Phone,
                CreateUserId = userId,
                CreateTime = DateTime.Now,
            };
            using (var tran = await _context.Database.BeginTransactionAsync())
            {
                _context.User.Add(user);
                _context.SaveChanges();
                if (created.RoleIds != null && created.RoleIds.Any())
                {
                    _context.UserRole.AddRange(created.RoleIds.Select(x => new UserRole
                    {
                        RoleId = x,
                        UserId = user.Id,
                    }));
                }
                await _context.SaveChangesAsync();
                await tran.CommitAsync();
            }
            return user;
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
                      orderby r.Id descending
                      select new UserListApiModel
                      {
                          Id = r.Id,
                          IsActive = r.IsActive,
                          Phone = r.Phone,
                          Username = r.Username,
                          CreateTime = r.CreateTime,
                          CreateUsername = p_tt.Username,
                      };
            if (query.Query?.IsActive!=null)
            {
                sql = sql.Where(x => x.IsActive == query.Query.IsActive);
            }
            if (!string.IsNullOrEmpty(query.Query?.Phone))
            {
                sql = sql.Where(x => x.Phone.Contains(query.Query.Phone));
            }
            if (!string.IsNullOrEmpty(query.Query?.Username))
            {
                sql = sql.Where(x => x.Username.Contains(query.Query.Username));
            }
            var data =  new PagerResult<UserListApiModel>(query.Index, query.Size, sql);
            foreach(var user in data.Result)
            {
                user.Roles = (from m in _context.SysRole
                              join p in _context.UserRole on new { RoleId = m.Id, UserId = user.Id } equals new { p.RoleId, p.UserId }
                              select new IdNameValueModel
                              {
                                  Name = m.Name,
                                  Id = m.Id,
                              }).ToList();
            }
            return data;
        }

        public User UpdateIsActive(int userId, bool isActive)
        {
            var user = _context.User.Where(x => x.Id == userId).FirstOrDefault();
            user.IsActive = isActive ? 1 : 0;
            _context.User.Update(user);
            _context.SaveChanges();
            return user;
        }

        public int Update(int id, UserUpdateApiModel updated)
        {
            var user = _context.User.First(x => x.Id == id);
            using (var tran = _context.Database.BeginTransaction())
            {
                user.Username = updated.Username;
                _context.User.Update(user);
                _context.SaveChanges();

                var roles = _context.UserRole.Where(x => x.UserId == id);
                _context.UserRole.RemoveRange(roles);
                _context.SaveChanges();

                if (updated.RoleIds != null && updated.RoleIds.Any())
                {
                    _context.UserRole.AddRange(updated.RoleIds.Select(x => new UserRole
                    {
                        RoleId = x,
                        UserId = id,
                    }));
                    _context.SaveChanges();
                }
                tran.Commit();
            }
            return user.Id;
        }

        public UserIndexApiModel GetIndex(int userId)
        {
            var user = _context.User.Where(x => x.Id == userId).Select(x => new UserIndexApiModel
            {
                Id = userId,
                Username = x.Username,
            }).First();

            user.Roles = (from m in _context.SysRole
                          join p in _context.UserRole on new { RoleId = m.Id, UserId = userId } equals new { p.RoleId, p.UserId } 
                          select new IdNameValueModel
                          {
                              Name = m.Name,
                              Id = m.Id,
                          }).ToList();
            return user;
        }
    
    
    }
}
