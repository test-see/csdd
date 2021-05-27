using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using storage.adapter.v2.client;
using storage.qurable.v2.client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace storage.v2.client
{
    public class Client2HospitalClientRespository : IClient2HospitalClientRespository
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
        public async Task<Client2HospitalClient> CreateAsync(Client2HospitalClient payload)
        {
            payload.CreateTime = DateTime.Now;
            _context.Client2HospitalClient.Add(payload);
            await _context.SaveChangesAsync();

            return payload;
        }

        public PagerResult<Client2HospitalClientOverview> ListOverviewByPage(PagerQuery<Client2HospitalClientQurable> payload)
        {
            var sql = from p in _context.Client2HospitalClient
                      join u in _context.User on p.CreateUserId equals u.Id
                      join c in _context.HospitalClient on p.HospitalClientId equals c.Id
                      join h in _context.Hospital on c.HospitalId equals h.Id
                      join ct in _context.Client on p.ClientId equals ct.Id
                      orderby p.Id descending
                      select new Client2HospitalClientOverview
                      {
                          Client = ct,
                          Mapping = p,
                          HospitalClient = c,
                          Hospital = h,
                          User = u,
                      };
            if (payload.Query?.HospitalId != null)
            {
                sql = sql.Where(x => x.Hospital.Id == payload.Query.HospitalId.Value);
            }
            if (payload.Query?.ClientId != null)
            {
                sql = sql.Where(x => x.Client.Id == payload.Query.ClientId.Value);
            }
            if (!string.IsNullOrEmpty(payload.Query?.Name))
            {
                sql = sql.Where(x => x.Client.Name.Contains(payload.Query.Name));
            }
            var data = new PagerResult<Client2HospitalClientOverview>(payload.Index, payload.Size, sql);

            return data;
        }
    }
}
