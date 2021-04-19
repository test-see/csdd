using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital;
using irespository.hospital.department.model;
using irespository.store.inout.profile.enums;
using irespository.storeinout;
using irespository.storeinout.model;
using Mediator.Net;
using storage.hospital.department.carrier;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace respository.store
{
    public class StoreInoutRespository : IStoreInoutRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IMediator _mediator;
        public StoreInoutRespository(DefaultDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<PagerResult<StoreInoutListApiModel>> GetPagerListAsync(PagerQuery<StoreInoutListQueryModel> query)
        {
            var sql = from r in _context.StoreInout
                      join u in _context.User on r.CreateUserId equals u.Id
                      join d in _context.DataStoreChangeType on r.ChangeTypeId equals d.Id
                      orderby r.Id descending
                      select new StoreInoutListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          CreateUserName = u.Username,
                          Name = r.Name,
                          Remark = r.Remark,
                          ChangeType = d,
                          Status = r.Status,
                          HospitalDepartment = new GetHospitalDepartmentResponse { Id = r.HospitalDepartmentId, }
                      };
            if (query.Query?.HospitalDepartmentId != null)
            {
                sql = sql.Where(x => x.HospitalDepartment.Id == query.Query.HospitalDepartmentId.Value);
            }
            if (query.Query?.ChangeTypeId != null)
            {
                sql = sql.Where(x => x.ChangeType.Id == query.Query.ChangeTypeId.Value);
            }
            if (query.Query?.Status != null)
            {
                sql = sql.Where(x => x.Status == query.Query.Status.Value);
            }
            if (!string.IsNullOrEmpty(query.Query?.Name))
            {
                sql = sql.Where(x => x.Name.Contains(query.Query.Name));
            }
            var data = new PagerResult<StoreInoutListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                var departments = await _mediator.RequestListByIdsAsync<GetHospitalDepartmentRequest, GetHospitalDepartmentResponse>(data.Select(x => x.HospitalDepartment.Id));
                foreach (var m in data.Result)
                {
                    m.HospitalDepartment = departments.FirstOrDefault(x => x.Id == m.HospitalDepartment.Id);
                }
            }
            return data;
        }

        public StoreInout Create(StoreInoutCreateApiModel created, int departmentId, int userId)
        {
            var setting = new StoreInout
            {
                HospitalDepartmentId = departmentId,
                CreateUserId = userId,
                CreateTime = DateTime.Now,
                Name = created.Name,
                Remark = created.Remark,
                ChangeTypeId = created.ChangeTypeId,
                Status = (int)StoreInoutStatus.Pendding,
            };

            _context.StoreInout.Add(setting);
            _context.SaveChanges();

            return setting;
        }

        public int Delete(int id)
        {
            var thresholds = _context.StoreInoutGoods.Where(x => x.StoreInoutId == id);
            if (thresholds.Any())
            {
                _context.StoreInoutGoods.RemoveRange(thresholds);
                _context.SaveChanges();
            }

            var setting = _context.StoreInout.Find(id);
            _context.StoreInout.Remove(setting);
            _context.SaveChanges();
            return id;
        }

        public int Update(int id, StoreInoutUpdateApiModel updated)
        {
            var setting = _context.StoreInout.First(x => x.Id == id);
            setting.Name = updated.Name;
            setting.Remark = updated.Remark;

            _context.StoreInout.Update(setting);
            _context.SaveChanges();
            return setting.Id;
        }

        public async Task<StoreInoutIndexApiModel> GetIndexAsync(int id)
        {
            var sql = from r in _context.StoreInout
                      join u in _context.User on r.CreateUserId equals u.Id
                      join d in _context.DataStoreChangeType on r.ChangeTypeId equals d.Id
                      where r.Id == id
                      select new StoreInoutIndexApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          CreateUserName = u.Username,
                          Name = r.Name,
                          Remark = r.Remark,
                          ChangeType = d,
                          Status = r.Status,
                          HospitalDepartment = new GetHospitalDepartmentResponse { Id = r.HospitalDepartmentId, }
                      };
            var data = sql.FirstOrDefault();
            if (data != null)
            {
                data.HospitalDepartment = await _mediator.RequestSingleByIdAsync<GetHospitalDepartmentRequest, GetHospitalDepartmentResponse>(data.HospitalDepartment.Id);
            }
            return data;
        }

        public int UpdateStatus(int id, StoreInoutStatus status)
        {
            var setting = _context.StoreInout.First(x => x.Id == id);
            setting.Status = (int)status;

            _context.StoreInout.Update(setting);
            _context.SaveChanges();
            return setting.Id;
        }
    }
}
