using foundation.ef5;
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
    }
}
