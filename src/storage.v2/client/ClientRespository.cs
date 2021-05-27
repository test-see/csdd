using foundation.ef5;
using foundation.ef5.poco;
using storage.adapter.v2.client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using foundation.config;

namespace storage.v2.client
{
    public class ClientRespository : IClientRespository
    {
        private readonly DefaultDbContext _context;
        public ClientRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<ClientOverview> GetOverviewByIdAsync(int id)
        {
            var sql = from r in _context.Client
                      join u in _context.User on r.CreateUserId equals u.Id
                      where r.Id == id
                      select new ClientOverview
                      {
                          Client = r,
                          User = u,
                      };
            return await sql.FirstOrDefaultAsync();
        }

        public PagerResult<ClientOverview> ListOverviewByPage(PagerQuery<ClientQurable> payload)
        {
            var sql = from r in _context.Client
                      join u in _context.User on r.CreateUserId equals u.Id
                      orderby r.Id descending
                      select new ClientOverview
                      {
                          Client = r,
                          User = u,
                      };
            if (!string.IsNullOrEmpty(payload.Query?.Name))
            {
                sql = sql.Where(x => x.Client.Name.Contains(payload.Query.Name));
            }
            var data = new PagerResult<ClientOverview>(payload.Index, payload.Size, sql);
            return data;
        }
    }
}
