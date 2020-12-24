using System;
using System.Collections.Generic;
using System.Linq;

namespace irespository.sys.model
{
    public class PrivilegeListApiModel
    {
        public string MenuName { get; set; }
        public string MenuPath { get; set; }
        public int ParentMenuId { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }

        public IList<PrivilegeListApiModel> Children { get; set; }
        public void FindChildren(IList<PrivilegeListApiModel> privileges)
        {
            this.Children = privileges.Where(x => x.ParentMenuId == MenuId).ToList();
            foreach (var m in this.Children)
            {
                m.FindChildren(privileges);
            }
        }
    }
}
