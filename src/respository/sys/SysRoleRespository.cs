using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
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
            var role = new SysRole { Name = created.RoleName, CreateUserId = userId, CreateTime = DateTime.UtcNow };
            using (var tran = _context.Database.BeginTransaction())
            {
                _context.SysRole.Add(role);

                if (created.MenuIds != null && created.MenuIds.Any())
                {
                    _context.SysPrivilege.AddRange(created.MenuIds.Select(x => new SysPrivilege
                    {
                        MenuId = x,
                        RoleId = role.Id,
                    }));
                }

                _context.SaveChanges();
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

        public RoleIndexApiModel GetRoleIndex(int roleId)
        {
            var role = _context.SysRole.Where(x => x.Id == roleId).Select(x => new RoleIndexApiModel
            {
                RoleId = roleId,
                RoleName = x.Name,
            }).First();

            role.MenuIds = (from p in _context.SysPrivilege
                            where p.RoleId == roleId
                            select p.MenuId).ToList();

            return role;
        }
        public IList<RoleMenuApiModel> GetMenuList()
        {
            var menus = from m in _context.DataMenu
                        orderby m.Rank
                        select new RoleMenuApiModel
                        {
                            MenuName = m.Name,
                            MenuPath = m.Path,
                            ParentMenuId = m.ParentId,
                            MenuId = m.Id,
                            IsCheck = false,
                        };
            return menus.ToList();
        }


        public int UpdateRole(RoleIndexUpdateModel updated)
        {
            var role = _context.SysRole.First(x => x.Id == updated.RoleId);
            using (var tran = _context.Database.BeginTransaction())
            {
                role.Name = updated.RoleName;
                _context.SysRole.Update(role);

                var privileges = _context.SysPrivilege.Where(x => x.RoleId == updated.RoleId);
                _context.SysPrivilege.RemoveRange(privileges);

                if (updated.MenuIds != null && updated.MenuIds.Any())
                {
                    _context.SysPrivilege.AddRange(updated.MenuIds.Select(x => new SysPrivilege
                    {
                        MenuId = x,
                        RoleId = updated.RoleId,
                    }));
                }
                _context.SaveChanges();
                tran.Commit();
            }
            return role.Id;
        }
    }
}
