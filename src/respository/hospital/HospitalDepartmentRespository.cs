using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.department.model;
using irespository.hospital.profile.model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace respository.hospital
{
    public class HospitalDepartmentRespository : IHospitalDepartmentRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalRespository _hospitalRespository;
        public HospitalDepartmentRespository(DefaultDbContext context,
            IHospitalRespository hospitalRespository)
        {
            _context = context;
            _hospitalRespository = hospitalRespository;
        }

        public PagerResult<HospitalDepartmentListApiModel> GetPagerList(PagerQuery<HospitalDepartmentListQueryModel> query)
        {
            var sql = from r in _context.HospitalDepartment
                      join u in _context.User on r.CreateUserId equals u.Id
                      join d in _context.DataDepartmentType on r.DepartmentTypeId equals d.Id
                      join rp in _context.HospitalDepartment on r.ParentId equals rp.Id into rp_def
                      from rp_def_t in rp_def.DefaultIfEmpty()
                      select new HospitalDepartmentListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Name = r.Name,
                          Hospital = new HospitalValueModel
                          {
                              Id = r.HospitalId
                          },
                          CreateUserName = u.Username,
                          DepartmentType = d,
                          IsPurchaseCheck = r.IsPurchaseCheck,
                          Parent = rp_def_t != null ? new IdNameValueModel { Id = rp_def_t.Id, Name = rp_def_t.Name } : null,
                      };
            if (query.Query?.HospitalId != null)
            {
                sql = sql.Where(x => x.Hospital.Id == query.Query.HospitalId.Value);
            }
            var data = new PagerResult<HospitalDepartmentListApiModel>(query.Index, query.Size, sql);
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

        public HospitalDepartment Create(HospitalDepartmentCreateApiModel created, int userId)
        {
            var goods = new HospitalDepartment
            {
                Name = created.Name,
                HospitalId = created.HospitalId,
                DepartmentTypeId = created.DepartmentTypeId,
                CreateUserId = userId,
                ParentId = created.ParentId,
                CreateTime = DateTime.Now,
                IsPurchaseCheck = created.IsPurchaseCheck,
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
            var department = _context.HospitalDepartment.First(x => x.Id == id);

            department.Name = updated.Name;
            department.DepartmentTypeId = updated.DepartmentTypeId;
            department.ParentId = updated.ParentId;
            department.IsPurchaseCheck = updated.IsPurchaseCheck;

            _context.HospitalDepartment.Update(department);
            _context.SaveChanges();
            return department.Id;
        }

        public IList<IdNameValueModel> GetParentList()
        {
            return _context.HospitalDepartment.Select(x => new IdNameValueModel { Id = x.Id, Name = x.Name }).ToList();
        }

        public HospitalDepartmentValueModel GetValue(int id)
        {
            var sql = from r in _context.HospitalDepartment
                      join d in _context.DataDepartmentType on r.DepartmentTypeId equals d.Id
                      where r.Id == id
                      select new HospitalDepartmentValueModel
                      {
                          Id = r.Id,
                          Name = r.Name,
                          Hospital = new HospitalValueModel
                          {
                              Id = r.HospitalId,
                          },
                          DepartmentType = d,
                      };
            var profile = sql.FirstOrDefault();
            if (profile != null)
            {
                profile.Hospital = _hospitalRespository.GetValue(new int[] { profile.Hospital.Id }).FirstOrDefault();
            }

            return profile;
        }
    }
}
