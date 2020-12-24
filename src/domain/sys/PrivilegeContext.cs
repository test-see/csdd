using irespository.sys;
using irespository.sys.model;
using System.Collections.Generic;
using System.Linq;

namespace domain.sys
{
    public class PrivilegeContext
    {
        private readonly ISysPrivilegeRespository _sysPrivilegeRespository;
        public PrivilegeContext(ISysPrivilegeRespository sysPrivilegeRespository)
        {
            _sysPrivilegeRespository = sysPrivilegeRespository;
        }

        public IEnumerable<PrivilegeListApiModel> GetPrivilegeList(int roleId)
        {
            var all = _sysPrivilegeRespository.GetPrivilegeList(roleId);
            var privileges = all.Where(x => x.ParentMenuId == 0);
            foreach(var m in privileges)
            {
                m.FindChildren(all.ToList());
            }
            return privileges;
        }      
    }
}
