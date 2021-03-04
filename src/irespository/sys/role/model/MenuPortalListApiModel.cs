using irespository.sys.model;
using System.Collections.Generic;

namespace irespository.sys.role.model
{
    public class MenuPortalListApiModel
    {
        public string PortalName { get; set; }
        public IList<RoleMenuApiModel> Menus { get; set; }
    }
}
