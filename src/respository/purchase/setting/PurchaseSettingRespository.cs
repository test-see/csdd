using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital.department.model;
using irespository.hospital.goods.model;
using irespository.hospital.profile.model;
using irespository.purchase;
using irespository.purchase.model;
using System;
using System.Linq;

namespace respository.purchase
{
    public class PurchaseSettingRespository : IPurchaseSettingRespository
    {
        private readonly DefaultDbContext _context;
        public PurchaseSettingRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public PagerResult<PurchaseSettingListApiModel> GetPagerList(PagerQuery<PurchaseSettingListQueryModel> query)
        {
            var sql = from r in _context.PurchaseSetting
                      join d in _context.HospitalDepartment on r.HospitalDepartmentId equals d.Id
                      join dt in _context.DataDepartmentType on d.DepartmentTypeId equals dt.Id
                      join h in _context.Hospital on d.HospitalId equals h.Id
                      join u in _context.User on r.CreateUserId equals u.Id
                      select new PurchaseSettingListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          CreateUserName = u.Username,
                          Name = r.Name,
                          Remark = r.Remark,
                          HospitalDepartment = new HospitalDepartmentValueModel
                          {
                              Id = d.Id,
                              Name = d.Name,
                              DepartmentType = dt,
                              Hospital = new HospitalValueModel
                              {
                                  Id = h.Id,
                                  Name = h.Name,
                                  Remark = h.Remark,
                              }
                          }
                      };
            return new PagerResult<PurchaseSettingListApiModel>(query.Index, query.Size, sql);
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
    }
}
