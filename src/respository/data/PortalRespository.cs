using foundation.ef5;
using foundation.ef5.poco;
using irespository.data;
using System.Collections.Generic;
using System.Linq;

namespace respository.data
{
    public class PortalRespository : IPortalRespository
    {
        private readonly DefaultDbContext _context;
        public PortalRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public IEnumerable<DataPortal> GetList()
        {
            return _context.DataPortal.ToList();
        }
    }
}
