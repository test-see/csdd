using irespository.data.model;
using System.Collections.Generic;

namespace irespository.sys.model
{
    public class RoleIndexApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<MenuValueModel> Menus { get; set; }
    }
}
