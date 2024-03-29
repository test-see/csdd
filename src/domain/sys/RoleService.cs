﻿using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;
using irespository.sys.role.model;
using irespository.user;
using irespository.user.enums;
using System.Collections.Generic;
using System.Linq;

namespace domain.sys
{
    public class RoleService
    {
        private readonly ISysRoleRespository _sysRoleRespository;
        public RoleService(ISysRoleRespository sysRoleRespository)
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
            var ps = menus.Select(x => new RoleMenuApiModel { IsCheck = false, Menu = x }).ToList();
            var result = new List<MenuPortalListApiModel>();
            var portals = menus.Select(x => x.Portal.Id).Distinct();
            foreach (var portal in portals)
            {
                var tops1 = menus.Where(x => x.ParentId == 0 && x.Portal.Id == portal).ToList();
                if (tops1.Any())
                {
                    var p = tops1.First().Portal;
                    var ts = tops1.Select(x => new RoleMenuApiModel { IsCheck = false, Menu = x }).ToList();

                    foreach (var menu in ts)
                    {
                        menu.FindChildren(ps);
                    }
                    result.Add(new MenuPortalListApiModel { Portal = p, Menus = ts });
                }
            }
            return result;
        }

        public IList<string> GetMenuListByUserId(int portalId, int userId)
        {
            var all = _sysRoleRespository.GetMenuList();
            var t = all.Where(x => x.Portal.Id == portalId).ToList();
            var menus = _sysRoleRespository.GetMenuListByUserId(portalId, userId);
            var append = menus.Select(x => x.Name).ToList();
            foreach (var m in menus)
            {
                var item = t.First(x => x.Id == m.Id);
                while (item.ParentId != 0)
                {
                    item = t.First(x => x.Id == item.ParentId);
                    append.Add(item.Name);
                }
            }
            return append.Distinct().ToList();
        }

    }
}
