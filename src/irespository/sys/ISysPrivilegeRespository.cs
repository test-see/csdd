using irespository.sys.model;
using System.Collections.Generic;

namespace irespository.sys
{
    public interface ISysPrivilegeRespository
    {
        IEnumerable<PrivilegeListApiModel> GetPrivilegeList(int roleId);
    }
}
