using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.client;
using irespository.client.model;
using System;
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
        public PagerResult<ClientListApiModel> GetPagerList(PagerQuery<ClientListQueryModel> query)
        {
            var sql = from r in _context.Client
                      join u in _context.User on r.CreateUserId equals u.Id
                      select new ClientListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Name = r.Name,
                          CreateUserName = u.Username,
                      };
            return new PagerResult<ClientListApiModel>(query.Index, query.Size, sql);
        }

        public Client Create(ClientCreateApiModel created, int userId)
        {
            var Client = new Client
            {
                Name = created.Name,
                CreateUserId = userId,
                CreateTime = DateTime.UtcNow
            };

            _context.Client.Add(Client);
            _context.SaveChanges();

            return Client;
        }

        public int Delete(int id)
        {
            var Client = _context.Client.Find(id);
            _context.Client.Remove(Client);
            _context.SaveChanges();
            return id;
        }

        public int Update(int id, ClientUpdateApiModel updated)
        {
            var Client = _context.Client.First(x => x.Id == id);
            Client.Name = updated.Name;

            _context.Client.Update(Client);
            _context.SaveChanges();
            return Client.Id;
        }
    }
}
