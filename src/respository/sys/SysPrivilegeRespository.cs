﻿using foundation.ef5;
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
            var privileges = from m in _context.DataMenu
                             join p in _context.SysPrivilege on new { MenuId = m.Id, RoleId = roleId } equals new { p.MenuId, p.RoleId } into p_t
                             from p_tt in p_t.DefaultIfEmpty()
                             select new PrivilegeListApiModel
                             {
                                 MenuName = m.Name,
                                 MenuPath = m.Path,
                                 ParentMenuId = m.ParentId,
                                 MenuId = m.Id,
                                 RoleId = p_tt.RoleId,
                                 IsCheck = p_tt != null,
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
