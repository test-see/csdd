using foundation.config;
using irespository.sys.model;
using System.Collections.Generic;

namespace irespository.sys.role.model
{
    public class MenuPortalListApiModel
    {
        public IdNameValueModel Portal { get; set; }
        public IList<RoleMenuApiModel> Menus { get; set; }
    }
}
