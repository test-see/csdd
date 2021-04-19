using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital;
using irespository.hospital.department.model;
using irespository.purchase;
using irespository.purchase.model;
using Mediator.Net;
using storage.hospital.department.carrier;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace respository.purchase
{
    public class PurchaseSettingRespository : IPurchaseSettingRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IMediator _mediator;
        public PurchaseSettingRespository(DefaultDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<PagerResult<PurchaseSettingListApiModel>> GetPagerListAsync(PagerQuery<PurchaseSettingListQueryModel> query, int hospitalId)
        {
            var sql = from r in _context.PurchaseSetting
                      join d in _context.HospitalDepartment on r.HospitalDepartmentId equals d.Id
                      join u in _context.User on r.CreateUserId equals u.Id
                      where d.HospitalId == hospitalId
                      orderby r.Id descending
                      select new PurchaseSettingListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          CreateUserName = u.Username,
                          Name = r.Name,
                          Remark = r.Remark,
                          HospitalDepartment = new GetHospitalDepartmentResponse { Id = r.HospitalDepartmentId, }
                      };
            if (query.Query?.HospitalDepartmentId != null)
            {
                sql = sql.Where(x => x.HospitalDepartment.Id == query.Query.HospitalDepartmentId.Value);
            }
            if (!string.IsNullOrEmpty(query.Query?.Name))
            {
                sql = sql.Where(x => x.Name.Contains(query.Query.Name));
            }
            var data = new PagerResult<PurchaseSettingListApiModel>(query.Index, query.Size, sql);
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

        public PurchaseSetting Create(PurchaseSettingCreateApiModel created, int departmentId, int userId)
        {
            var setting = new PurchaseSetting
            {
                HospitalDepartmentId = departmentId,
                CreateUserId = userId,
                CreateTime = DateTime.Now,
                Name = created.Name,
                Remark = created.Remark,
            };

            _context.PurchaseSetting.Add(setting);
            _context.SaveChanges();

            return setting;
        }

        public int Delete(int id)
        {
            var thresholds = _context.PurchaseSettingThreshold.Where(x => x.PurchaseSettingId == id);
            if (thresholds.Any())
            {
                _context.PurchaseSettingThreshold.RemoveRange(thresholds);
                _context.SaveChanges();
            }

            var setting = _context.PurchaseSetting.Find(id);
            _context.PurchaseSetting.Remove(setting);
            _context.SaveChanges();
            return id;
        }

        public int Update(int id, PurchaseSettingUpdateApiModel updated)
        {
            var setting = _context.PurchaseSetting.First(x => x.Id == id);
            setting.Name = updated.Name;
            setting.Remark = updated.Remark;

            _context.PurchaseSetting.Update(setting);
            _context.SaveChanges();
            return setting.Id;
        }

        public async Task<PurchaseSettingIndexApiModel> GetIndexAsync(int id)
        {
            var sql = from r in _context.PurchaseSetting
                      join u in _context.User on r.CreateUserId equals u.Id
                      select new PurchaseSettingIndexApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          CreateUserName = u.Username,
                          Name = r.Name,
                          Remark = r.Remark,
                          HospitalDepartment = new GetHospitalDepartmentResponse
                          {
                              Id = r.HospitalDepartmentId,
                          }
                      };
            var setting =  sql.FirstOrDefault();
            if (setting != null)
            {
                setting.HospitalDepartment = await _mediator.RequestSingleByIdAsync<GetHospitalDepartmentRequest, GetHospitalDepartmentResponse>(setting.HospitalDepartment.Id);
            }
            return setting;
        }

    }
}
