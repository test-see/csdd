using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.department.model;
using irespository.checklist;
using irespository.checklist.model;
using irespository.checklist.profile.enums;
using System;
using System.Linq;

namespace respository.checklist
{
    public class CheckListRespository : ICheckListRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalDepartmentRespository _hospitalDepartmentRespository;
        public CheckListRespository(DefaultDbContext context,
            IHospitalDepartmentRespository hospitalDepartmentRespository)
        {
            _context = context;
            _hospitalDepartmentRespository = hospitalDepartmentRespository;
        }
        public PagerResult<CheckListApiModel> GetPagerList(PagerQuery<CheckListQueryModel> query, int hospitalId)
        {
            var sql = from r in _context.CheckList
                      join u in _context.User on r.CreateUserId equals u.Id
                      join d in _context.HospitalDepartment on r.HospitalDepartmentId equals d.Id
                      where d.HospitalId == hospitalId
                      orderby r.Id descending
                      select new CheckListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          CreateUserName = u.Username,
                          Name = r.Name,
                          Remark = r.Remark,
                          Status = r.Status,
                          HospitalDepartment = new GetHospitalDepartmentResponse { Id = r.HospitalDepartmentId, }
                      };
            if (query.Query?.HospitalDepartmentId != null)
            {
                sql = sql.Where(x => x.HospitalDepartment.Id == query.Query.HospitalDepartmentId.Value);
            }
            if (query.Query?.Status != null)
            {
                sql = sql.Where(x => x.Status == query.Query.Status.Value);
            }
            if (!string.IsNullOrEmpty(query.Query?.Name))
            {
                sql = sql.Where(x => x.Name.Contains(query.Query.Name));
            }
            var data = new PagerResult<CheckListApiModel>(query.Index, query.Size, sql);
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

        public CheckList Create(CheckListCreateApiModel created, int userId)
        {
            var setting = new CheckList
            {
                HospitalDepartmentId = created.HospitalDepartmentId,
                CreateUserId = userId,
                CreateTime = DateTime.Now,
                Name = created.Name,
                Remark = created.Remark,
                Status = (int)CheckListStatus.Pendding,
                 
            };

            _context.CheckList.Add(setting);
            _context.SaveChanges();

            return setting;
        }

        public int Delete(int id)
        {
            var thresholds = _context.CheckListGoods.Where(x => x.CheckListId == id);
            if (thresholds.Any())
            {
                _context.CheckListGoods.RemoveRange(thresholds);
                _context.SaveChanges();
            }

            var setting = _context.CheckList.Find(id);
            _context.CheckList.Remove(setting);
            _context.SaveChanges();
            return id;
        }

        public int Update(int id, CheckListUpdateApiModel updated)
        {
            var setting = _context.CheckList.First(x => x.Id == id);
            setting.Name = updated.Name;
            setting.Remark = updated.Remark;

            _context.CheckList.Update(setting);
            _context.SaveChanges();
            return setting.Id;
        }

        public int UpdateStatus(int id, CheckListStatus status)
        {
            var setting = _context.CheckList.First(x => x.Id == id);
            setting.Status = (int)status;

            _context.CheckList.Update(setting);
            _context.SaveChanges();
            return setting.Id;
        }

        public CheckListIndexApiModel GetIndex(int id)
        {
            var sql = from r in _context.CheckList
                      join u in _context.User on r.CreateUserId equals u.Id
                      where r.Id == id
                      select new CheckListIndexApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          CreateUserName = u.Username,
                          Name = r.Name,
                          Remark = r.Remark,
                          Status = r.Status,
                          HospitalDepartment = new GetHospitalDepartmentResponse
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
