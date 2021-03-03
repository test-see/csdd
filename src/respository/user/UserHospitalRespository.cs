using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
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
        private readonly IHospitalDepartmentRespository _hospitalDepartmentRespository;
        public UserHospitalRespository(DefaultDbContext context,
            IHospitalDepartmentRespository hospitalDepartmentRespository)
        {
            _context = context;
            _hospitalDepartmentRespository = hospitalDepartmentRespository;
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
                              Id = r.HospitalDepartmentId,
                          }
                      };
            var data = new PagerResult<UserHospitalListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                var departments = _hospitalDepartmentRespository.GetValue(data.Result.Select(x => x.HospitalDepartment.Id).ToArray());
                foreach (var m in data.Result)
                {
                    m.HospitalDepartment = departments.FirstOrDefault(x => x.Id == m.HospitalDepartment.Id);
                }
            }
            return data;
        }


        public UserHospitalIndexApiModel GetIndexByUserId(int userId)
        {
            var sql = from r in _context.UserHospital
                      join u in _context.User on r.CreateUserId equals u.Id
                      join s in _context.User on r.UserId equals s.Id
                      where r.UserId == userId
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
                              Id = r.HospitalDepartmentId,
                          }
                      };
            var user = sql.FirstOrDefault();
            if (user != null)
            {
                user.HospitalDepartment = _hospitalDepartmentRespository.GetValue(new int[] { user.HospitalDepartment.Id }).FirstOrDefault();
            }
            return user;
        }
    }
}
