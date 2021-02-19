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
        public HospitalClientRespository(DefaultDbContext context)
        {
            _context = context;
        }

        public PagerResult<HospitalClientListApiModel> GetPagerList(PagerQuery<HospitalClientListQueryModel> query)
        {
            var sql = from r in _context.HospitalClient
                      join u in _context.User on r.CreateUserId equals u.Id
                      join h in _context.Hospital on r.HospitalId equals h.Id
                      select new HospitalClientListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Name = r.Name,
                          Hospital = new HospitalValueModel
                          {
                              Id = h.Id,
                              Name = h.Name,
                              Remark = h.Remark,
                          },
                          CreateUserName = u.Username,
                      };
            if (query.Query?.HospitalId != null)
            {
                sql = sql.Where(x => x.Hospital.Id == query.Query.HospitalId.Value);
            }
            return new PagerResult<HospitalClientListApiModel>(query.Index, query.Size, sql);
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
                          join h in _context.Hospital on r.HospitalId equals h.Id
                          where r.Id == id
                          select new HospitalClientValueModel
                          {
                              Id = r.Id,
                              Name = r.Name,
                              Hospital = new HospitalValueModel
                              {
                                  Id = h.Id,
                                  Name = h.Name,
                                  Remark = h.Remark,
                              },
                          }).First();

            var sql = from p in _context.ClientMapping
                      join c in _context.Client on p.ClientId equals c.Id
                      where p.HospitalClientId == id
                      select new ClientValueModel
                      {
                          Id = c.Id,
                          Name = c.Name,
                      };
            client.Client = sql.FirstOrDefault();

            return client;
        }
    }
}
