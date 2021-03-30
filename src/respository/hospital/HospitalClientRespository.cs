using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.client.profile.model;
using irespository.hospital;
using irespository.hospital.client.model;
using irespository.hospital.profile.model;
using System;
using System.Collections.Generic;
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
                      orderby r.Id descending
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
            if (!string.IsNullOrEmpty(query.Query?.Name))
            {
                sql = sql.Where(x => x.Name.Contains(query.Query.Name));
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
        public IList<HospitalClientValueModel> GetValue(int[] ids)
        {
            if (ids.Length == 0) return new List<HospitalClientValueModel>();
            var hospitalClients = (from r in _context.HospitalClient
                                   join u in _context.User on r.CreateUserId equals u.Id
                                   join m in _context.ClientMapping on r.Id equals m.HospitalClientId into md
                                   from mdd in md.DefaultIfEmpty()
                                   where ids.Contains(r.Id)
                                   select new HospitalClientValueModel
                                   {
                                       Id = r.Id,
                                       Name = r.Name,
                                       Hospital = new HospitalValueModel
                                       {
                                           Id = r.HospitalId,
                                       },
                                       Client = mdd != null ? new ClientValueModel { Id = mdd.ClientId } : null,
                                   }).ToList();

            var dd = hospitalClients.Where(x => x.Client != null).Select(x => x.Client.Id).ToList();
            var sql = (from c in _context.Client
                       where dd.Contains(c.Id)
                       select new ClientValueModel
                       {
                           Id = c.Id,
                           Name = c.Name,
                       }).ToList();
            var hospitals = _hospitalRespository.GetValue(hospitalClients.Select(x => x.Hospital.Id).ToArray());
            foreach (var c in hospitalClients)
            {
                if (c.Client != null) c.Client = sql.FirstOrDefault(x => x.Id == c.Client.Id);
                c.Hospital = hospitals.FirstOrDefault(x => x.Id == c.Hospital.Id);
            }

            return hospitalClients;
        }
    }
}
