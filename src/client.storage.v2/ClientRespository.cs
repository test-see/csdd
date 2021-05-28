using client.storage.qurable.v2.data;
using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using Microsoft.EntityFrameworkCore;
using storage.adapter.v2.client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public PagerResult<ClientOverview> GetOverviewByPage(PagerQuery<ClientQurable> payload)
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

        public async Task<Client> CreateAsync(Client payload)
        {
            payload.CreateTime = DateTime.Now;
            _context.Client.Add(payload);
            await _context.SaveChangesAsync();
            return payload;
        }
        public async Task<Client> UpdateAsync(Client payload)
        {
            var data = _context.Client.First(x => x.Id == payload.Id);
            data.Name = payload.Name;

            _context.Client.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }
        public async Task DeleteAsync(int id)
        {
            var data = _context.Client.Find(id);
            _context.Client.Remove(data);
            await _context.SaveChangesAsync();
        }
    }
}
