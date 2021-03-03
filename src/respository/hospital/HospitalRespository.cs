using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.model;
using irespository.hospital.profile.model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace respository.hospital
{
    public class HospitalRespository: IHospitalRespository
    {
        private readonly DefaultDbContext _context;
        public HospitalRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public PagerResult<HospitalListApiModel> GetPagerList(PagerQuery<HospitalListQueryModel> query)
        {
            var sql = from r in _context.Hospital
                      join u in _context.User on r.CreateUserId equals u.Id
                      select new HospitalListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Name = r.Name,
                          Remark = r.Remark,
                          CreateUserName = u.Username,
                          ConsumeDays = r.ConsumeDays,
                      };
            return new PagerResult<HospitalListApiModel>(query.Index, query.Size, sql);
        }

        public Hospital Create(HospitalCreateApiModel created, int userId)
        {
            var hospital = new Hospital
            {
                Name = created.Name,
                Remark = created.Remark,
                ConsumeDays = created.ConsumeDays,
                CreateUserId = userId,
                CreateTime = DateTime.Now,
            };

            _context.Hospital.Add(hospital);
            _context.SaveChanges();

            return hospital;
        }

        public int Delete(int id)
        {
            var hospital = _context.Hospital.Find(id);
            _context.Hospital.Remove(hospital);
            _context.SaveChanges();
            return id;
        }

        public int Update(int id, HospitalUpdateApiModel updated)
        {
            var hospital = _context.Hospital.First(x => x.Id ==id);
            hospital.Name = updated.Name;
            hospital.Remark = updated.Remark;
            hospital.ConsumeDays = updated.ConsumeDays;

            _context.Hospital.Update(hospital);
            _context.SaveChanges();
            return hospital.Id;
        }

        public IList<HospitalValueModel> GetValue(int[] ids)
        {
            if (ids.Length == 0) return new List<HospitalValueModel>();
            return _context.Hospital.Where(x => ids.Contains(x.Id)).Select(x => new HospitalValueModel
            {
                ConsumeDays = x.ConsumeDays,
                Id = x.Id,
                Name = x.Name,
                Remark = x.Remark,
            }).ToList();
        }
    }
}
