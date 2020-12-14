using foundation.ef5.poco;
using irespository.sys.model;
using irespository.user;
using System.Collections.Generic;

namespace domain.sys
{
    public class RoleContext
    {
        private readonly ISysRoleRespository _sysRoleRespository;
        public RoleContext(ISysRoleRespository sysRoleRespository)
        {
            _sysRoleRespository = sysRoleRespository;
        }

        public IEnumerable<RoleListApiModel> GetList()
        {
            return _sysRoleRespository.GetList();
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
