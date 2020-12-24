using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.sys.model;
using irespository.user;
using System;
using System.Linq;

namespace respository.sys
{
    public class SysRoleRespository : ISysRoleRespository
    {
        private readonly DefaultDbContext _context;
        public SysRoleRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public SysRole Create(string name, int userId)
        {
            var role = new SysRole { Name = name, CreateUserId = userId, CreateTime = DateTime.UtcNow };
            _context.SysRole.Add(role);
            _context.SaveChanges();
            return role;
        }

        public int Delete(int id)
        {
            var role = _context.SysRole.Find(id);
            _context.SysRole.Remove(role);
            _context.SaveChanges();
            return id;
        }

        public PagerResult<RoleListApiModel> GetPagerList(PagerQuery<RoleListQueryModel> query)
        {
            var sql = from r in _context.SysRole
                        join u in _context.User on r.CreateUserId equals u.Id
                        select new RoleListApiModel
                        {
                            CreateTime = r.CreateTime,
                            Id = r.Id,
                            Name = r.Name,
                            CreateUserName = u.Phone,
                        };
            return new PagerResult<RoleListApiModel>(query.Index, query.Size, sql);
        }
    }
}
