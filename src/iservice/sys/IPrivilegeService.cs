using domain.sys.entities;
using System.Collections.Generic;

namespace iservice.sys
{
    public interface IPrivilegeService
    {
        IEnumerable<MenuEntity> GetMenuList();
    }
}
