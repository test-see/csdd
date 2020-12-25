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
            var roles = from p in _context.UserRole
                             join m in _context.SysRole on p.RoleId equals m.Id
                             where p.UserId == userId
                             select new UserRoleListApiModel
                             {
                                 RoleName = m.Name,
                                 RoleId = p.RoleId,
                                 UserId = p.UserId,
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
