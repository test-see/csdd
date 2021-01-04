using foundation.ef5.poco;
using System.Collections.Generic;

namespace irespository.data
{
    public interface IAuthorizeRoleRespository
    {
        IEnumerable<DataAuthorizeRole> GetList();
    }
}
