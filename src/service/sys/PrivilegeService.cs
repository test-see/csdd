using domain.sys;
using irespository.sys.model;
using iservice.sys;
using System.Collections.Generic;

namespace service.sys
{
    public class PrivilegeService: IPrivilegeService
    {
        private readonly PrivilegeContext _privilegeContext;
        public PrivilegeService(PrivilegeContext privilegeContext)
        {
            _privilegeContext = privilegeContext;
        }
        public IEnumerable<PrivilegeListApiModel> GetPrivilegeList(int roleId)
        {
            return _privilegeContext.GetPrivilegeList(roleId);
        }
    }
}
