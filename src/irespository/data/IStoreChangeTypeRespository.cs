using foundation.ef5.poco;
using System.Collections.Generic;

namespace irespository.data
{
    public interface IStoreChangeTypeRespository
    {
        DataStoreChangeType GetIndex(int id);
        IEnumerable<DataStoreChangeType> GetCustomizeList();
    }
}
