using foundation.ef5;
using storage.adapter.v2.client;
using System.Linq;
using System.Threading.Tasks;

namespace storage.v2.client
{
    public class Client2HospitalClientRespository: IClient2HospitalClientRespository
    {
        private readonly DefaultDbContext _context;
        public Client2HospitalClientRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task DeleteAllByClientIdAsync(int clientId)
        {
            var data = _context.Client2HospitalClient.Where(x => x.ClientId == clientId);
            _context.Client2HospitalClient.RemoveRange(data);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var data = _context.Client2HospitalClient.Find(id);
            _context.Client2HospitalClient.Remove(data);
            await _context.SaveChangesAsync();
        }
    }
}
