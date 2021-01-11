using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;
using System.Collections.Generic;

namespace iservice.sys
{
    public interface IRoleService
    {
        PagerResult<RoleListApiModel> GetPagerList(PagerQuery<RoleListQueryModel> query);
        SysRole Create(RoleCreateApiModel created, int userId);
        int Delete(int id);
        RoleIndexApiModel GetIndex(int roleId);
        int UpdateRole(RoleIndexUpdateModel updated);
        IList<RoleMenuApiModel> GetMenuList();
    }
}
