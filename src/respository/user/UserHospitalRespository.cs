﻿using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital;
using irespository.hospital.department.model;
using irespository.hospital.profile.model;
using irespository.user.hospital;
using irespository.user.hospital.model;
using irespository.user.profile.model;
using Mediator.Net;
using storage.hospital.department.carrier;
using storage.hospitaldepartment.carrier;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace respository.user
{
    public class UserHospitalRespository : IUserHospitalRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IMediator _mediator;
        public UserHospitalRespository(DefaultDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
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

        public async Task<PagerResult<UserHospitalListApiModel>> GetPagerListAsync(PagerQuery<UserHospitalListQueryModel> query)
        {
            var sql = from r in _context.UserHospital
                      join u in _context.User on r.CreateUserId equals u.Id
                      join s in _context.User on r.UserId equals s.Id
                      orderby r.Id descending
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
                          HospitalDepartment = new GetHospitalDepartmentResponse
                          {
                              Id = r.HospitalDepartmentId,
                          }
                      };
            if (query.Query != null && query.Query.HospitalId != null)
            {
               var departments= await _mediator.ListAsync<ListHospitalDepartmentRequest, ListHospitalDepartmentResponse>(new ListHospitalDepartmentRequest { 
                 HospitalId= query.Query.HospitalId.Value,
                });
                sql = sql.Where(x => departments.Select(x => x.Id).ToList().Contains(x.HospitalDepartment.Id));
            }
            if (!string.IsNullOrEmpty(query.Query?.Phone))
            {
                sql = sql.Where(x => x.User.Phone.Contains(query.Query.Phone));
            }
            var data = new PagerResult<UserHospitalListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                var departments = await _mediator.ListByIdsAsync<GetHospitalDepartmentRequest, GetHospitalDepartmentResponse>(data.Select(x => x.HospitalDepartment.Id));
                foreach (var m in data.Result)
                {
                    m.HospitalDepartment = departments.FirstOrDefault(x => x.Id == m.HospitalDepartment.Id);
                }
            }
            return data;
        }


        public async Task<UserHospitalIndexApiModel> GetIndexByUserIdAsync(int userId)
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
                          HospitalDepartment = new GetHospitalDepartmentResponse
                          {
                              Id = r.HospitalDepartmentId,
                          }
                      };
            var user = sql.FirstOrDefault();
            if (user != null)
            {
                user.HospitalDepartment = await _mediator.GetByIdAsync<GetHospitalDepartmentRequest, GetHospitalDepartmentResponse>(user.HospitalDepartment.Id);
            }
            return user;
        }
    }
}
