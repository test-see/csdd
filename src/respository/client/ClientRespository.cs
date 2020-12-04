using foundation.ef5;
using foundation.ef5.poco;
using irespository.client;
using System.Collections.Generic;
using System.Linq;

namespace respository.client
{
    public class ClientRespository : IClientRespository
    {
        private readonly DefaultDbContext _context;
        public ClientRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Client> GetListByProvince(int provinceId)
        {
            return _context.Client.Where(x => x.ProvinceId == provinceId).ToList();
        }
    }
}
