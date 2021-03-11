using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.data.model;
using irespository.sys.model;
using irespository.user;
using System;
using System.Collections.Generic;
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
        public SysRole Create(RoleCreateApiModel created, int userId)
        {
            var role = new SysRole { Name = created.Name, CreateUserId = userId, CreateTime = DateTime.UtcNow };
            using (var tran = _context.Database.BeginTransaction())
            {
                _context.SysRole.Add(role);
                _context.SaveChanges();

                if (created.MenuIds != null && created.MenuIds.Any())
                {
                    _context.SysPrivilege.AddRange(created.MenuIds.Select(x => new SysPrivilege
                    {
                        MenuId = x,
                        RoleId = role.Id,
                    }));
                    _context.SaveChanges();
                }

                tran.Commit();
            }
            return role;
        }

        public int Delete(int id)
        {
            var privileges = _context.SysPrivilege.Where(x => x.RoleId == id);
            _context.SysPrivilege.RemoveRange(privileges);
            var userroles = _context.UserRole.Where(x => x.RoleId == id);
            _context.UserRole.RemoveRange(userroles);

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
                            CreateUserName = u.Username,
                        };
            return new PagerResult<RoleListApiModel>(query.Index, query.Size, sql);
        }

        public RoleIndexApiModel GetIndex(int roleId)
        {
            var role = _context.SysRole.Where(x => x.Id == roleId).Select(x => new RoleIndexApiModel
            {
                Id = roleId,
                Name = x.Name,
            }).First();

            role.Menus = (from p in _context.SysPrivilege
                          join m in _context.DataMenu on p.MenuId equals m.Id
                          join xp in _context.DataPortal on m.PortalId equals xp.Id
                          where p.RoleId == roleId
                          select new MenuValueModel
                          {
                              Name = m.Name,
                              DisplayName = m.DisplayName,
                              Path = m.Path,
                              ParentId = m.ParentId,
                              Id = m.Id,
                              Icon = m.Icon,
                              HideChildrenInMenu = m.IsHideChildren > 0,
                              HideInMenu = m.IsHide > 0,
                              Portal = new IdNameValueModel { Id = xp.Id, Name = xp.Name, },
                          }).ToList();

            return role;
        }
        public IList<MenuValueModel> GetMenuList()
        {
            var menus = from m in _context.DataMenu
                        join p in _context.DataPortal on m.PortalId equals p.Id
                        orderby m.Rank
                        select new MenuValueModel
                        {
                            Name = m.Name,
                            DisplayName = m.DisplayName,
                            Path = m.Path,
                            ParentId = m.ParentId,
                            Id = m.Id,
                            Icon = m.Icon,
                            HideChildrenInMenu = m.IsHideChildren > 0,
                            HideInMenu = m.IsHide > 0,
                            Portal = new IdNameValueModel { Id = p.Id, Name = p.Name, },

                        };
            return menus.ToList();
        }

        public IList<MenuValueModel> GetMenuListByUserId(int portalId, int userId)
        {
            var menus = from m in _context.DataMenu
                        join d in _context.DataPortal on m.PortalId equals d.Id
                        join p in _context.SysPrivilege on m.Id equals p.MenuId
                        join u in _context.UserRole on p.RoleId equals u.RoleId
                        where u.UserId == userId && m.PortalId == portalId
                        orderby m.Rank
                        select new MenuValueModel
                        {
                            Name = m.Name,
                            DisplayName = m.DisplayName,
                            Path = m.Path,
                            ParentId = m.ParentId,
                            Id = m.Id,
                            Icon = m.Icon,
                            HideChildrenInMenu = m.IsHideChildren > 0,
                            HideInMenu = m.IsHide > 0,
                            Portal = new IdNameValueModel { Id = d.Id, Name = d.Name, },
                        };
            return menus.Distinct().ToList();
        }


        public int Update(int id, RoleIndexUpdateModel updated)
        {
            var role = _context.SysRole.First(x => x.Id == id);
            using (var tran = _context.Database.BeginTransaction())
            {
                role.Name = updated.Name;
                _context.SysRole.Update(role);

                var privileges = _context.SysPrivilege.Where(x => x.RoleId == id);
                _context.SysPrivilege.RemoveRange(privileges);

                if (updated.MenuIds != null && updated.MenuIds.Any())
                {
                    _context.SysPrivilege.AddRange(updated.MenuIds.Select(x => new SysPrivilege
                    {
                        MenuId = x,
                        RoleId = id,
                    }));
                }
                _context.SaveChanges();
                tran.Commit();
            }
            return role.Id;
        }
    }
}
