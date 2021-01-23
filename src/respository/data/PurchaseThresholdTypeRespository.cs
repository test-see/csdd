using foundation.ef5;
using foundation.ef5.poco;
using irespository.data;
using System.Collections.Generic;
using System.Linq;

namespace respository.data
{
    public class PurchaseThresholdTypeRespository : IPurchaseThresholdTypeRespository
    {
        private readonly DefaultDbContext _context;
        public PurchaseThresholdTypeRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public IEnumerable<DataPurchaseThresholdType> GetList()
        {
            return _context.DataPurchaseThresholdType.ToList();
        }
    }
}
