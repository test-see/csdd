using Microsoft.EntityFrameworkCore.Storage;

namespace foundation.ef5
{
    public class DefaultDbTransaction
    {
        private readonly DefaultDbContext _context;
        public DefaultDbTransaction(DefaultDbContext context)
        {
            _context = context;
        }

        public IDbContextTransaction Begin()
        {
            return _context.Database.BeginTransaction();
        }
    }
}
