using foundation.ef5.poco;
using irespository.store.record;

namespace domain.store
{
    public class StoreRecordBillnoContext
    {
        private readonly IStoreRecordBillnoRespository _storeRecordBillnoRespository;
        public StoreRecordBillnoContext(IStoreRecordBillnoRespository storeRecordBillnoRespository)
        {
            _storeRecordBillnoRespository = storeRecordBillnoRespository;
        }

        public StoreRecordBillno Create(int billnoId, int recordId)
        {
            return _storeRecordBillnoRespository.Create(billnoId, recordId);
        }
    }
}
