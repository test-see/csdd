using foundation.ef5.poco;

namespace irespository.data
{
    public interface IStoreChangeTypeRespository
    {
        DataStoreChangeType GetIndex(int id);
    }
}
