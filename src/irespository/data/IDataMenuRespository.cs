using foundation.ef5.poco;
using System.Collections.Generic;

namespace irespository.user
{
    public interface IDataMenuRespository
    {
        IEnumerable<DataMenu> GetList();
    }
}
