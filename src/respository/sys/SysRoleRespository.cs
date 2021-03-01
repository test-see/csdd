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
                            Menu = new MenuValueModel
                            {
                                Name = m.Name,
                                Path = m.Path,
                                ParentId = m.ParentId,
                                Id = m.Id,
                            },
                            IsCheck = false,
                        };
            return menus.ToList();
        }

        public IList<RoleMenuApiModel> GetMenuListByUserId(int userId)
        {
            var menus = from m in _context.DataMenu
                        join p in _context.SysPrivilege on m.Id equals p.MenuId
                        join u in _context.UserRole on p.RoleId equals u.RoleId
                        where u.UserId == userId
                        orderby m.Rank
                        select new RoleMenuApiModel
                        {
                            Menu = new MenuValueModel
                            {
                                Name = m.Name,
                                Path = m.Path,
                                ParentId = m.ParentId,
                                Id = m.Id,
                            },
                            IsCheck = true,
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
