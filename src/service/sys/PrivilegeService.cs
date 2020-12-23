using domain.sys;
using domain.sys.entities;
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
        public IEnumerable<MenuEntity> GetList()
        {
            return _privilegeContext.GetList();
        }
    }
}
