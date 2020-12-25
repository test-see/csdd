using foundation.ef5;
using foundation.ef5.poco;
using irespository.sys.model;
using irespository.user;
using System;
using System.Collections.Generic;
using System.Linq;

namespace respository.user
{
    public class UserRoleRespository : IUserRoleRespository
    {
        private readonly DefaultDbContext _context;
        public UserRoleRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public IEnumerable<UserRoleListApiModel> GetUserRoleList(int userId)
        {
            var roles = from m in _context.SysRole
                        join p in _context.UserRole on new { RoleId = m.Id, UserId = userId } equals new { p.RoleId, p.UserId } into m_t
                        from m_tt in m_t.DefaultIfEmpty()
                        select new UserRoleListApiModel
                        {
                            RoleName = m.Name,
                            RoleId = m.Id,
                            UserId = m_tt.UserId,
                            IsCheck = m_tt != null,
                        };

            return roles.ToList();
        }

        public int UpdateUserRoleList(UserRoleListUpdateModel updated)
        {
            var roles = _context.UserRole.Where(x => x.UserId == updated.UserId);
            _context.UserRole.RemoveRange(roles);

            _context.UserRole.AddRange(updated.RoleIds.Select(x => new UserRole
            {
                RoleId = x,
                UserId = updated.UserId,
            }));

            _context.SaveChanges();
            return updated.RoleIds.Count;
        }
    }
}
