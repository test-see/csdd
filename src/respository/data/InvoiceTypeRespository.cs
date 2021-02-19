using foundation.ef5;
using foundation.ef5.poco;
using irespository.data;
using System.Collections.Generic;
using System.Linq;

namespace respository.data
{
    public class InvoiceTypeRespository : IInvoiceTypeRespository
    {
        private readonly DefaultDbContext _context;
        public InvoiceTypeRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public IEnumerable<DataInvoiceType> GetList()
        {
            return _context.DataInvoiceType.ToList();
        }
    }
}
