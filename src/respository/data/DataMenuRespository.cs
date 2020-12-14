using foundation.ef5;
using foundation.ef5.poco;
using irespository.user;
using System.Collections.Generic;
using System.Linq;

namespace respository.data
{
    public class DataMenuRespository : IDataMenuRespository
    {
        private readonly DefaultDbContext _context;
        public DataMenuRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public IEnumerable<DataMenu> GetList()
        {
            return _context.DataMenu.ToList();
        }
    }
}
