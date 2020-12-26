using irespository.sys.model;
using System.Collections.Generic;

namespace iservice.sys
{
    public interface IPrivilegeService
    {
        IEnumerable<RoleMenuApiModel> GetPrivilegeList(int roleId);
        int UpdatePrivilegeList(RoleIndexUpdateModel privileges);
    }
}
