using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.client.profile.model;
using irespository.hospital;
using irespository.hospital.client.model;
using irespository.hospital.profile.model;
using System;
using System.Linq;

namespace respository.hospital
{
    public class HospitalClientRespository : IHospitalClientRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalRespository _hospitalRespository;
        public HospitalClientRespository(DefaultDbContext context,
            IHospitalRespository hospitalRespository)
        {
            _context = context;
            _hospitalRespository = hospitalRespository;
        }

        public PagerResult<HospitalClientListApiModel> GetPagerList(PagerQuery<HospitalClientListQueryModel> query)
        {
            var sql = from r in _context.HospitalClient
                      join u in _context.User on r.CreateUserId equals u.Id
                      select new HospitalClientListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Name = r.Name,
                          Hospital = new HospitalValueModel
                          {
                              Id = r.HospitalId,
                          },
                          CreateUserName = u.Username,
                      };
            if (query.Query?.HospitalId != null)
            {
                sql = sql.Where(x => x.Hospital.Id == query.Query.HospitalId.Value);
            }
            var data = new PagerResult<HospitalClientListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                var hospitals = _hospitalRespository.GetValue(data.Result.Select(x => x.Hospital.Id).ToArray());
                foreach (var m in data.Result)
                {
                    m.Hospital = hospitals.FirstOrDefault(x => x.Id == m.Hospital.Id);
                }
            }
            return data;
        }


        public HospitalClient Create(HospitalClientCreateApiModel created, int userId)
        {
            var goods = new HospitalClient
            {
                Name = created.Name,
                HospitalId = created.HospitalId,
                CreateUserId = userId,
                CreateTime = DateTime.Now,
            };

            _context.HospitalClient.Add(goods);
            _context.SaveChanges();

            return goods;
        }



        public int Delete(int id)
        {
            var goods = _context.HospitalClient.Find(id);
            _context.HospitalClient.Remove(goods);
            _context.SaveChanges();
            return id;
        }

        public int Update(int id, HospitalClientUpdateApiModel updated)
        {
            var goods = _context.HospitalClient.First(x => x.Id == id);

            goods.Name = updated.Name;

            _context.HospitalClient.Update(goods);
            _context.SaveChanges();
            return goods.Id;
        }
        public HospitalClientValueModel GetValue(int id)
        {
            var client = (from r in _context.HospitalClient
                          join u in _context.User on r.CreateUserId equals u.Id
                          where r.Id == id
                          select new HospitalClientValueModel
                          {
                              Id = r.Id,
                              Name = r.Name,
                              Hospital = new HospitalValueModel
                              {
                                  Id = r.HospitalId,
                              },
                          }).FirstOrDefault();

            if (client != null)
            {
                var sql = from p in _context.ClientMapping
                          join c in _context.Client on p.ClientId equals c.Id
                          where p.HospitalClientId == id
                          select new ClientValueModel
                          {
                              Id = c.Id,
                              Name = c.Name,
                          };
                client.Client = sql.FirstOrDefault();
                client.Hospital = _hospitalRespository.GetValue(new int[] { client.Hospital.Id }).FirstOrDefault();
            }

            return client;
        }
    }
}
