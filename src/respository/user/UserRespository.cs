﻿using foundation.config;
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
        public async Task<User> AddActiveUserAsync(UserCreateApiModel created, int userId)
        {
            var user = new User
            {
                IsActive = 1,
                Username = created.Username,
                Phone = created.Phone,
                CreateTime = DateTime.UtcNow,
                CreateUserId = userId,
                AuthorizeRoleId = created.AuthorizeRoleId,
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

        public int UpdateUser(UserUpdateApiModel updated)
        {
            var user = _context.User.First(x => x.Id == updated.UserId);
            using (var tran = _context.Database.BeginTransaction())
            {
                user.Username = updated.Username;
                user.AuthorizeRoleId = updated.AuthorizeRoleId;
                _context.User.Update(user);

                var roles = _context.UserRole.Where(x => x.UserId == updated.UserId);
                _context.UserRole.RemoveRange(roles);

                if (updated.RoleIds != null && updated.RoleIds.Any())
                {
                    _context.UserRole.AddRange(updated.RoleIds.Select(x => new UserRole
                    {
                        RoleId = x,
                        UserId = updated.UserId,
                    }));
                }
                _context.SaveChanges();
                tran.Commit();
            }
            return user.Id;
        }

        public UserIndexApiModel GetUserIndex(int userId)
        {
            var user = _context.User.Where(x => x.Id == userId).Select(x => new UserIndexApiModel
            {
                UserId = userId,
                Username = x.Username,
            }).First();

            user.Roles = (from m in _context.SysRole
                          join p in _context.UserRole on new { RoleId = m.Id, UserId = userId } equals new { p.RoleId, p.UserId } 
                          select new UserRoleIndexApiModel
                          {
                              RoleName = m.Name,
                              RoleId = m.Id,
                          }).ToList();
            return user;
        }
    }
}
