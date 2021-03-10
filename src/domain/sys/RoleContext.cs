using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;
using irespository.sys.role.model;
using irespository.user;
using irespository.user.enums;
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

        public RoleIndexApiModel GetIndex(int roleId)
        {
            return _sysRoleRespository.GetIndex(roleId);
        }
        public int Update(int id, RoleIndexUpdateModel updated)
        {
            return _sysRoleRespository.Update(id, updated);
        }

        public IList<MenuPortalListApiModel> GetMenuList()
        {
            var menus = _sysRoleRespository.GetMenuList();
            var result = new List<MenuPortalListApiModel>();
            var portals = menus.Select(x => x.Menu.Portal.Id).Distinct();
            foreach (var portal in portals)
            {
                var tops1 = menus.Where(x => x.Menu.ParentId == 0 && x.Menu.Portal.Id == portal).ToList();
                if (tops1.Any())
                {
                    var p = tops1.First().Menu.Portal;
                    foreach (var menu in tops1)
                    {
                        menu.FindChildren(menus);
                    }
                    result.Add(new MenuPortalListApiModel { Portal = p, Menus = tops1 });
                }
            }
            return result;
        }


        public IList<RoleMenuApiModel> GetMenuListByUserId(int authorizeRoleId, int userId)
        {
            var menus = _sysRoleRespository.GetMenuListByUserId(authorizeRoleId, userId);
            var tops = menus.Where(x => x.Menu.ParentId == 0).ToList();
            foreach (var menu in tops)
            {
                menu.FindChildren(menus);
            }
            return tops;
        }

    }
}
