using foundation.ef5;
using foundation.ef5.poco;
using irespository.store.record;

namespace respository.store
{
    public class StoreRecordBillnoRespository : IStoreRecordBillnoRespository
    {
        private readonly DefaultDbContext _context;
        public StoreRecordBillnoRespository(DefaultDbContext context)
        {
            _context = context;
        }

        public StoreRecordBillno Create(int billnoId, int recordId)
        {
            var record = new StoreRecordBillno
            {
                PurchaseGoodsBillnoId = billnoId,
                StoreRecordId = recordId,
            };

            _context.StoreRecordBillno.Add(record);
            _context.SaveChanges();

            return record;
        }

    }
}
