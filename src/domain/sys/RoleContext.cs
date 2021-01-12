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

        public RoleIndexApiModel GetIndex(int roleId)
        {
            return _sysRoleRespository.GetIndex(roleId);
        }
        public int Update(int id, RoleIndexUpdateModel updated)
        {
            return _sysRoleRespository.Update(id, updated);
        }

        public IList<RoleMenuApiModel> GetMenuList()
        {
            return _sysRoleRespository.GetMenuList();
        }
    }
}
