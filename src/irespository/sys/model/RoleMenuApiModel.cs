using System.Collections.Generic;
using System.Linq;

namespace irespository.sys.model
{
    public class RoleMenuApiModel
    {
        public string MenuName { get; set; }
        public string MenuPath { get; set; }
        public int ParentMenuId { get; set; }
        public int MenuId { get; set; }
        public bool IsCheck { get; set; }
        public IList<RoleMenuApiModel> Children { get; set; }
        public void FindChildren(IList<RoleMenuApiModel> privileges)
        {
            this.Children = privileges.Where(x => x.ParentMenuId == MenuId).ToList();
            foreach (var m in this.Children)
            {
                m.FindChildren(privileges);
            }
        }
    }
}
