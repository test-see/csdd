using foundation.ef5;
using foundation.ef5.poco;
using irespository.data;
using System.Linq;

namespace respository.data
{
    public class StoreChangeTypeRespository : IStoreChangeTypeRespository
    {
        private readonly DefaultDbContext _context;
        public StoreChangeTypeRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public DataStoreChangeType GetIndex(int id)
        {
            return _context.DataStoreChangeType.Find(id);
        }
    }
}
