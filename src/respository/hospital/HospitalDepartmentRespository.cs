﻿using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.department.model;
using irespository.hospital.profile.model;
using System;
using System.Linq;

namespace respository.hospital
{
    public class HospitalDepartmentRespository : IHospitalDepartmentRespository
    {
        private readonly DefaultDbContext _context;
        public HospitalDepartmentRespository(DefaultDbContext context)
        {
            _context = context;
        }

        public PagerResult<HospitalDepartmentListApiModel> GetPagerList(PagerQuery<HospitalDepartmentListQueryModel> query)
        {
            var sql = from r in _context.HospitalDepartment
                      join u in _context.User on r.CreateUserId equals u.Id
                      join h in _context.Hospital on r.HospitalId equals h.Id
                      select new HospitalDepartmentListApiModel
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
            return new PagerResult<HospitalDepartmentListApiModel>(query.Index, query.Size, sql);
        }

        public HospitalDepartment Create(HospitalDepartmentCreateApiModel created, int userId)
        {
            var goods = new HospitalDepartment
            {
                Name = created.Name,
                HospitalId = created.HospitalId,
                CreateUserId = userId,
                CreateTime = DateTime.UtcNow,
            };

            _context.HospitalDepartment.Add(goods);
            _context.SaveChanges();

            return goods;
        }



        public int Delete(int id)
        {
            var goods = _context.HospitalDepartment.Find(id);
            _context.HospitalDepartment.Remove(goods);
            _context.SaveChanges();
            return id;
        }

        public int Update(int id, HospitalDepartmentUpdateApiModel updated)
        {
            var goods = _context.HospitalDepartment.First(x => x.Id == id);

            goods.Name = updated.Name;

            _context.HospitalDepartment.Update(goods);
            _context.SaveChanges();
            return goods.Id;
        }
    }
}
