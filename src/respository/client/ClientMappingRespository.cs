using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.client.maping;
using irespository.client.maping.model;
using irespository.client.maping.profile.model;
using irespository.client.profile.model;
using irespository.hospital;
using irespository.hospital.client.model;
using irespository.hospital.profile.model;
using System;
using System.Linq;

namespace respository.client
{
    public class ClientMappingRespository : IClientMappingRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalClientRespository _hospitalClientRespository;
        public ClientMappingRespository(DefaultDbContext context,
            IHospitalClientRespository hospitalClientRespository)
        {
            _context = context;
            _hospitalClientRespository = hospitalClientRespository;
        }
        public ClientMapping Create(ClientMappingCreateApiModel created, int userId)
        {
            var mapping = new ClientMapping
            {
                HospitalClientId = created.HospitalClientId,
                ClientId = created.ClientId,
                CreateUserId = userId,
                CreateTime = DateTime.Now,
            };
            _context.ClientMapping.Add(mapping);
            _context.SaveChanges();

            return mapping;
        }

        public int Delete(int id)
        {
            var mapping = _context.ClientMapping.Find(id);
            _context.ClientMapping.Remove(mapping);
            _context.SaveChanges();
            return id;
        }

        public PagerResult<ClientMappingListApiModel> GetPagerList(PagerQuery<ClientMappingListQueryModel> query)
        {
            var sql = from p in _context.ClientMapping
                      join u in _context.User on p.CreateUserId equals u.Id
                      join c in _context.HospitalClient on p.HospitalClientId equals c.Id
                      join h in _context.Hospital on c.HospitalId equals h.Id
                      join ct in _context.Client on p.ClientId equals ct.Id
                      orderby p.Id descending
                      select new ClientMappingListApiModel
                      {
                          Client = new ClientValueModel
                          {
                              Id = ct.Id,
                              Name = ct.Name,
                          },
                          ClientMappingId = p.Id,
                          HospitalClient = new HospitalClientValueModel
                          {
                              Id = c.Id,
                              Hospital = new HospitalValueModel { Id = h.Id, }
                          },
                          CreateTime = p.CreateTime,
                          CreateUserName = u.Username,
                      };
            if (query.Query?.HospitalId != null)
            {
                sql = sql.Where(x => x.HospitalClient.Hospital.Id == query.Query.HospitalId.Value);
            }
            if (query.Query?.ClientId != null)
            {
                sql = sql.Where(x => x.Client.Id == query.Query.ClientId.Value);
            }
            if (!string.IsNullOrEmpty(query.Query?.Name))
            {
                sql = sql.Where(x => x.Client.Name.Contains(query.Query.Name));
            }
            var data = new PagerResult<ClientMappingListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                var hospitals = _hospitalClientRespository.GetValue(data.Result.Select(x => x.HospitalClient.Id).ToArray());
                foreach (var m in data.Result)
                {
                    m.HospitalClient = hospitals.FirstOrDefault(x => x.Id == m.HospitalClient.Id);
                }
            }
            return data;
        }
    }
}
