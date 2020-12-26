using System.Collections.Generic;

namespace irespository.sys.model
{
    public class RoleIndexApiModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public IList<RoleMenuApiModel> Menus { get; set; }
    }
}
