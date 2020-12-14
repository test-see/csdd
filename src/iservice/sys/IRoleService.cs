using foundation.ef5.poco;
using irespository.sys.model;
using System.Collections.Generic;

namespace iservice.sys
{
    public interface IRoleService
    {
        IEnumerable<RoleListApiModel> GetList();
        SysRole Create(string name, int userId);
        int Delete(int id);
    }
}
