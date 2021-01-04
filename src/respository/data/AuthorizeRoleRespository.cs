using foundation.ef5;
using foundation.ef5.poco;
using irespository.data;
using System.Collections.Generic;
using System.Linq;

namespace respository.data
{
    public class AuthorizeRoleRespository: IAuthorizeRoleRespository
    {
        private readonly DefaultDbContext _context;
        public AuthorizeRoleRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public IEnumerable<DataAuthorizeRole> GetList()
        {
            return _context.DataAuthorizeRole.ToList();
        }
    }
}
