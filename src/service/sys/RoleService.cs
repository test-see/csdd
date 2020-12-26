using domain.sys;
using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;
using iservice.sys;
using System.Collections.Generic;

namespace service.sys
{
    public class RoleService : IRoleService
    {
        private readonly RoleContext _roleContext;
        public RoleService(RoleContext roleContext)
        {
            _roleContext = roleContext;
        }
        public SysRole Create(RoleCreateApiModel created, int userId)
        {
            return _roleContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _roleContext.Delete(id);
        }

        public PagerResult<RoleListApiModel> GetPagerList(PagerQuery<RoleListQueryModel> query)
        {
            return _roleContext.GetPagerList(query);
        }
        public RoleIndexApiModel GetRoleIndex(int roleId)
        {
            return _roleContext.GetRoleIndex(roleId);
        }

        public int UpdateRole(RoleIndexUpdateModel updated)
        {
            return _roleContext.UpdateRole(updated);
        }

        public IList<RoleMenuApiModel> GetMenuList()
        {
            return _roleContext.GetMenuList();
        }
    }
}
