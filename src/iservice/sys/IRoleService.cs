using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;

namespace iservice.sys
{
    public interface IRoleService
    {
        PagerResult<RoleListApiModel> GetPagerList(PagerQuery<RoleListQueryModel> query);
        SysRole Create(RoleCreateApiModel created, int userId);
        int Delete(int id);
        RoleIndexApiModel GetRoleIndex(int roleId);
        int UpdateRole(RoleIndexUpdateModel updated);
    }
}
