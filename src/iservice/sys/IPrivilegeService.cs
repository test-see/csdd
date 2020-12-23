using foundation.ef5.poco;
using System.Collections.Generic;

namespace iservice.sys
{
    public interface IPrivilegeService
    {
        IEnumerable<DataMenu> GetList();
    }
}
