using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.department.model;
using irespository.purchase;
using irespository.purchase.model;
using System;
using System.Linq;

namespace respository.purchase
{
    public class PurchaseSettingRespository : IPurchaseSettingRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalDepartmentRespository _hospitalDepartmentRespository;
        public PurchaseSettingRespository(DefaultDbContext context,
            IHospitalDepartmentRespository hospitalDepartmentRespository)
        {
            _context = context;
            _hospitalDepartmentRespository = hospitalDepartmentRespository;
        }
        public PagerResult<PurchaseSettingListApiModel> GetPagerList(PagerQuery<PurchaseSettingListQueryModel> query)
        {
            var sql = from r in _context.PurchaseSetting
                      join u in _context.User on r.CreateUserId equals u.Id
                      select new PurchaseSettingListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          CreateUserName = u.Username,
                          Name = r.Name,
                          Remark = r.Remark,
                          HospitalDepartment = new HospitalDepartmentValueModel { Id = r.HospitalDepartmentId, }
                      };
            var data = new PagerResult<PurchaseSettingListApiModel>(query.Index, query.Size, sql);
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
            _context.PurchaseSettingThreshold.RemoveRange(thresholds);
            _context.SaveChanges();

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

        public PurchaseSettingIndexApiModel GetIndex(int id)
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
                          HospitalDepartment = new HospitalDepartmentValueModel
                          {
                              Id = r.HospitalDepartmentId,
                          }
                      };
            var setting =  sql.FirstOrDefault();
            if (setting != null)
            {
                setting.HospitalDepartment = _hospitalDepartmentRespository.GetValue(new int[] { setting.HospitalDepartment.Id }).FirstOrDefault();
            }
            return setting;
        }

    }
}
