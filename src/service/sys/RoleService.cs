using domain.sys;
using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;
using iservice.sys;

namespace service.sys
{
    public class RoleService : IRoleService
    {
        private readonly RoleContext _roleContext;
        public RoleService(RoleContext roleContext)
        {
            _roleContext = roleContext;
        }
        public SysRole Create(string name, int userId)
        {
            return _roleContext.Create(name, userId);
        }

        public int Delete(int id)
        {
            return _roleContext.Delete(id);
        }

        public PagerResult<RoleListApiModel> GetPagerList(PagerQuery<RoleListQueryModel> query)
        {
            return _roleContext.GetPagerList(query);
        }
    }
}
