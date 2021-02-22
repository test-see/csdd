using foundation.ef5.poco;

namespace irespository.store.record
{
    public interface IStoreRecordBillnoRespository
    {
        StoreRecordBillno Create(int billnoId, int recordId);
    }
}
