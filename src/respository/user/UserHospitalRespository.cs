using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital.department.model;
using irespository.hospital.profile.model;
using irespository.user.hospital;
using irespository.user.hospital.model;
using irespository.user.profile.model;
using System;
using System.Linq;

namespace respository.user
{
    public class UserHospitalRespository : IUserHospitalRespository
    {
        private readonly DefaultDbContext _context;
        public UserHospitalRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public UserHospital Create(UserHospitalCreateApiModel created, int userId)
        {
            var user = new UserHospital
            {
                Name = created.Name,
                CreateUserId = userId,
                HospitalDepartmentId = created.HospitalDepartmentId,
                UserId = created.UserId,
                CreateTime = DateTime.Now,
            };

            _context.UserHospital.Add(user);
            _context.SaveChanges();

            return user;
        }

        public int Delete(int id)
        {
            var user = _context.UserHospital.Find(id);
            _context.UserHospital.Remove(user);
            _context.SaveChanges();
            return id;
        }

        public PagerResult<UserHospitalListApiModel> GetPagerList(PagerQuery<UserHospitalListQueryModel> query)
        {
            var sql = from r in _context.UserHospital
                      join u in _context.User on r.CreateUserId equals u.Id
                      join s in _context.User on r.UserId equals s.Id
                      join h in _context.HospitalDepartment on r.HospitalDepartmentId equals h.Id
                      join d in _context.DataDepartmentType on h.DepartmentTypeId equals d.Id
                      join o in _context.Hospital on h.HospitalId equals o.Id
                      select new UserHospitalListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Name = r.Name,
                          CreateUserName = u.Username,
                          User = new UserValueModel
                          {
                              Id = s.Id,
                              Phone = s.Phone,
                              Username = s.Username,
                          },
                          HospitalDepartment = new HospitalDepartmentValueModel
                          {
                              Id = h.Id,
                              Name = h.Name,
                              DepartmentType = d,
                              Hospital = new HospitalValueModel
                              {
                                  Id = o.Id,
                                  Name = o.Name,
                                  Remark = o.Remark,
                              }
                          }
                      };
            return new PagerResult<UserHospitalListApiModel>(query.Index, query.Size, sql);
        }


        public UserHospitalIndexApiModel GetIndexByUserId(int userId)
        {
            var sql = from r in _context.UserHospital
                      join u in _context.User on r.CreateUserId equals u.Id
                      join s in _context.User on r.UserId equals s.Id
                      join h in _context.HospitalDepartment on r.HospitalDepartmentId equals h.Id
                      join d in _context.DataDepartmentType on h.DepartmentTypeId equals d.Id
                      where r.UserId == userId
                      join o in _context.Hospital on h.HospitalId equals o.Id
                      select new UserHospitalIndexApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Name = r.Name,
                          CreateUserName = u.Username,
                          User = new UserValueModel
                          {
                              Id = s.Id,
                              Phone = s.Phone,
                              Username = s.Username,
                          },
                          HospitalDepartment = new HospitalDepartmentValueModel
                          {
                              Id = h.Id,
                              Name = h.Name,
                              DepartmentType = d,
                              Hospital = new HospitalValueModel
                              {
                                  Id = o.Id,
                                  Name = o.Name,
                                  Remark = o.Remark,
                              }
                          }
                      };
            return sql.FirstOrDefault();
        }
    }
}
