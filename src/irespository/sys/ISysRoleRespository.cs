using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;

namespace irespository.user
{
    public interface ISysRoleRespository
    {
        PagerResult<RoleListApiModel, RoleListQueryModel> GetPagerList(RoleListQueryModel query);
        SysRole Create(string name, int userId);
        int Delete(int id);
    }
}
