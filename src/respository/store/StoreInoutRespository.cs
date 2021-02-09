using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.department.model;
using irespository.storeinout;
using irespository.storeinout.model;
using System;
using System.Linq;

namespace respository.store
{
    public class StoreInoutRespository : IStoreInoutRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalDepartmentRespository _hospitalDepartmentRespository;
        public StoreInoutRespository(DefaultDbContext context,
            IHospitalDepartmentRespository hospitalDepartmentRespository)
        {
            _context = context;
            _hospitalDepartmentRespository = hospitalDepartmentRespository;
        }
        public PagerResult<StoreInoutListApiModel> GetPagerList(PagerQuery<StoreInoutListQueryModel> query)
        {
            var sql = from r in _context.StoreInout
                      join u in _context.User on r.CreateUserId equals u.Id
                      select new StoreInoutListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          CreateUserName = u.Username,
                          Name = r.Name,
                          Remark = r.Remark,
                          HospitalDepartment = new HospitalDepartmentValueModel { Id = r.HospitalDepartmentId, }
                      };
            var data = new PagerResult<StoreInoutListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                foreach (var m in data.Result)
                {
                    m.HospitalDepartment = _hospitalDepartmentRespository.GetValue(m.HospitalDepartment.Id);
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
                Status = 0,
            };

            _context.StoreInout.Add(setting);
            _context.SaveChanges();

            return setting;
        }

        public int Delete(int id)
        {
            var thresholds = _context.StoreInoutGoods.Where(x => x.StoreInoutId == id);
            _context.StoreInoutGoods.RemoveRange(thresholds);
            _context.SaveChanges();

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

    }
}
