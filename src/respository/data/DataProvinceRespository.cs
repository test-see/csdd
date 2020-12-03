using foundation.ef5;
using foundation.ef5.poco;
using irespository.user;
using System.Collections.Generic;
using System.Linq;

namespace respository.data
{
    public class DataProvinceRespository : IDataProvinceRespository
    {
        private readonly DefaultDbContext _context;
        public DataProvinceRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public IEnumerable<DataProvince> GetList()
        {
            return _context.DataProvince.ToList();
        }
    }
}
