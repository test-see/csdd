using foundation.ef5;
using foundation.ef5.poco;
using irespository.data;
using System.Collections.Generic;
using System.Linq;

namespace respository.data
{
    public class DepartmentTypeRespository : IDepartmentTypeRespository
    {
        private readonly DefaultDbContext _context;
        public DepartmentTypeRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public IEnumerable<DataDepartmentType> GetList()
        {
            return _context.DataDepartmentType.ToList();
        }
    }
}
