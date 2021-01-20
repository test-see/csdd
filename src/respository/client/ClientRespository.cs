using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.client;
using irespository.client.model;
using irespository.hospital.client.model;
using irespository.hospital.profile.model;
using System;
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
            var client = new Client
            {
                Name = created.Name,
                CreateUserId = userId,
                CreateTime = DateTime.Now,
            };

            _context.Client.Add(client);
            _context.SaveChanges();


            return client;
        }

        public int Delete(int id)
        {
            var mappings = _context.ClientMapping.Where(x => x.ClientId == id);
            _context.ClientMapping.RemoveRange(mappings);

            var Client = _context.Client.Find(id);
            _context.Client.Remove(Client);
            _context.SaveChanges();
            return id;
        }

        public int Update(int id, ClientUpdateApiModel updated, int userId)
        {
            var client = _context.Client.First(x => x.Id == id);
            client.Name = updated.Name;

            _context.Client.Update(client);
            _context.SaveChanges();

            return client.Id;
        }



        public ClientIndexApiModel GetIndex(int id)
        {
            var role = _context.Client.Where(x => x.Id == id).Select(x => new ClientIndexApiModel
            {
                Id = x.Id,
                Name = x.Name,
            }).First();

            var sql = from p in _context.ClientMapping
                      join c in _context.HospitalClient on p.HospitalClientId equals c.Id
                      join h in _context.Hospital on c.HospitalId equals h.Id
                      where p.ClientId == id
                      select new KeyValuePair<int, HospitalClientValueModel>(

                          p.Id,
                          new HospitalClientValueModel
                          {
                              Id = c.Id,
                              Name = c.Name,
                              Hospital = new HospitalValueModel
                              {
                                  Id = h.Id,
                                  Name = h.Name,
                                  Remark = h.Remark,
                              }
                          });
            role.HospitalClients = sql.ToList();
            return role;
        }
    
    
    }
}
