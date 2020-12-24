using foundation.ef5;
using foundation.ef5.poco;
using irespository.sys;
using irespository.sys.model;
using System.Collections.Generic;
using System.Linq;

namespace respository.sys
{
    public class SysPrivilegeRespository : ISysPrivilegeRespository
    {
        private readonly DefaultDbContext _context;
        public SysPrivilegeRespository(DefaultDbContext context)
        {
            _context = context;
        }

        public IEnumerable<PrivilegeListApiModel> GetPrivilegeList(int roleId)
        {
            var privileges = from p in _context.SysPrivilege
                             join m in _context.DataMenu on p.MenuId equals m.Id
                             where p.RoleId == roleId
                             select new PrivilegeListApiModel
                             {
                                 MenuName = m.Name,
                                 MenuPath = m.Path,
                                 ParentMenuId = m.ParentId,
                                 RoleId = p.RoleId,
                                 MenuId = p.MenuId,
                             };

            return privileges.ToList();
        }

        public int UpdatePrivilegeList(PrivilegeListUpdateModel updated)
        {
            var privileges = _context.SysPrivilege.Where(x => x.RoleId == updated.RoleId);
            _context.SysPrivilege.RemoveRange(privileges);

            _context.SysPrivilege.AddRange(updated.MenuIds.Select(x => new SysPrivilege
            {
                MenuId = x,
                RoleId = updated.RoleId,
            }));

            _context.SaveChanges();
            return updated.MenuIds.Count;
        }
    }
}
