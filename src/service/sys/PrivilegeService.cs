using domain.sys;
using foundation.ef5.poco;
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
        public IEnumerable<DataMenu> GetList()
        {
            return _privilegeContext.GetList();
        }
    }
}
