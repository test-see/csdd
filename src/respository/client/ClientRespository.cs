using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.client;
using irespository.client.maping.model;
using irespository.client.model;
using irespository.client.profile.model;
using irespository.hospital;
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
        private readonly IHospitalClientRespository _hospitalClientRespository;
        public ClientRespository(DefaultDbContext context,
            IHospitalClientRespository hospitalClientRespository)
        {
            _context = context;
            _hospitalClientRespository = hospitalClientRespository;
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
            var client = (from r in _context.Client
                          join u in _context.User on r.CreateUserId equals u.Id
                          where r.Id == id
                          select new ClientIndexApiModel
                          {
                              CreateTime = r.CreateTime,
                              Id = r.Id,
                              Name = r.Name,
                              CreateUserName = u.Username,
                          }).First();

            var sql = from p in _context.ClientMapping
                      join c in _context.HospitalClient on p.HospitalClientId equals c.Id
                      join h in _context.Hospital on c.HospitalId equals h.Id
                      where p.ClientId == id
                      select new ClientMappingListApiModel
                      {
                          Client = new ClientValueModel
                          {
                              Id = client.Id,
                              Name = client.Name,
                          },
                          ClientMappingId = p.Id,
                          HospitalClient = new HospitalClientValueModel
                          {
                              Id = c.Id,
                          },
                      };
            client.HospitalClients = sql.ToList();
            var clients = _hospitalClientRespository.GetValue(client.HospitalClients.Select(x => x.HospitalClient.Id).ToArray());
            foreach (var m in client.HospitalClients)
            {
                m.HospitalClient = clients.FirstOrDefault(x => x.Id == m.HospitalClient.Id);
            }

            return client;
        }

        public IList<ClientValueModel> GetValue(int[] ids)
        {
            if (ids.Length == 0) return new List<ClientValueModel>();
            var client = (from r in _context.Client
                          join u in _context.User on r.CreateUserId equals u.Id
                          where ids.Contains(r.Id)
                          select new ClientValueModel
                          {
                              Id = r.Id,
                              Name = r.Name,
                          }).ToList();

            return client;
        }


    }
}
