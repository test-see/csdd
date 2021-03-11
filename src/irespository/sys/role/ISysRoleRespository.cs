using foundation.config;
using foundation.ef5.poco;
using irespository.data.model;
using irespository.sys.model;
using System.Collections.Generic;

namespace irespository.user
{
    public interface ISysRoleRespository
    {
        PagerResult<RoleListApiModel> GetPagerList(PagerQuery<RoleListQueryModel> query);
        SysRole Create(RoleCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, RoleIndexUpdateModel updated);
        RoleIndexApiModel GetIndex(int roleId);
        IList<MenuValueModel> GetMenuList();
        IList<MenuValueModel> GetMenuListByUserId(int portalId, int userId);
    }
}
