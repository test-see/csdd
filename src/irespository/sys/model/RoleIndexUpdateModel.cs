using System.Collections.Generic;

namespace irespository.sys.model
{
    public class RoleIndexUpdateModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public IList<int> MenuIds { get; set; }
    }
}
