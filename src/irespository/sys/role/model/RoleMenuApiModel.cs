using irespository.data.model;
using System.Collections.Generic;
using System.Linq;

namespace irespository.sys.model
{
    public class RoleMenuApiModel
    {
        public MenuValueModel Menu { get; set; }
        public bool IsCheck { get; set; }
        public IList<RoleMenuApiModel> Children { get; set; }
        public void FindChildren(IList<RoleMenuApiModel> privileges)
        {
            this.Children = privileges.Where(x => x.Menu.ParentId == Menu.Id).ToList();
            foreach (var m in this.Children)
            {
                m.FindChildren(privileges);
            }
        }
    }
}
