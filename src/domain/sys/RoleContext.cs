using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;
using irespository.user;
using System.Collections.Generic;
using System.Linq;

namespace domain.sys
{
    public class RoleContext
    {
        private readonly ISysRoleRespository _sysRoleRespository;
        public RoleContext(ISysRoleRespository sysRoleRespository)
        {
            _sysRoleRespository = sysRoleRespository;
        }

        public PagerResult<RoleListApiModel> GetPagerList(PagerQuery<RoleListQueryModel> query)
        {
            return _sysRoleRespository.GetPagerList(query);
        }
        public SysRole Create(RoleCreateApiModel created, int userId)
        {
            return _sysRoleRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _sysRoleRespository.Delete(id);
        }

        public RoleIndexApiModel GetRoleIndex(int roleId)
        {
            var role = _sysRoleRespository.GetRoleIndex(roleId);
            var menus = role.Menus.Where(x => x.ParentMenuId == 0);
            foreach (var m in menus)
            {
                m.FindChildren(role.Menus.ToList());
            }
            role.Menus = menus.ToList();
            return role;
        }
        public int UpdateRole(RoleIndexUpdateModel updated)
        {
            return _sysRoleRespository.UpdateRole(updated);
        }

        public IList<RoleMenuApiModel> GetMenuList()
        {
            return _sysRoleRespository.GetMenuList();
        }
    }
}
