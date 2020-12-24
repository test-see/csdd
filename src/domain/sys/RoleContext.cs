using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;
using irespository.user;

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
        public SysRole Create(string name, int userId)
        {
            return _sysRoleRespository.Create(name, userId);
        }
        public int Delete(int id)
        {
            return _sysRoleRespository.Delete(id);
        }
    }
}
