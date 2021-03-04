using foundation.ef5.poco;
using System.Collections.Generic;

namespace iservice.data
{
    public interface IDataService
    {
        IEnumerable<DataPortal> GetDataAuthorizeList();
    }
}
