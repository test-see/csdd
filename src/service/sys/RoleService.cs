using domain.sys;
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
        public SysRole Create(string name, int userId)
        {
            return _roleContext.Create(name, userId);
        }

        public int Delete(int id)
        {
            return _roleContext.Delete(id);
        }

        public IEnumerable<RoleListApiModel> GetList()
        {
            return _roleContext.GetList();
        }
    }
}
