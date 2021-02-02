using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.prescription;
using irespository.prescription.model;
using System.Linq;

namespace respository.prescription
{
    public class PrescriptionRespository : IPrescriptionRespository
    {
        private readonly DefaultDbContext _context;
        public PrescriptionRespository(DefaultDbContext context)
        {
            _context = context;
        }

        public Prescription Create(PrescriptionCreateApiModel created, int departmentId, int userId)
        {
            throw new System.NotImplementedException();
        }

        public PagerResult<PrescriptionListApiModel> GetPagerList(PagerQuery<PrescriptionListQueryModel> query)
        {
            var sql = from p in _context.Prescription
                      //join d in _context.HospitalDepartment on p.HospitalDepartmentId equals d.Id
                      //join pg in _context.PrescriptionGoods on p.Id equals pg.PrescriptionId
                      //join dt in _context.DataDepartmentType on d.DepartmentTypeId equals dt.Id
                      //join h in _context.Hospital on d.HospitalId equals h.Id
                      join u in _context.User on p.CreateUserId equals u.Id
                      select new PrescriptionListApiModel
                      {
                          //CreateTime = r.CreateTime,
                          //Id = r.Id,
                          //CreateUserName = u.Username,
                          //Name = r.Name,
                          //Remark = r.Remark,
                          //HospitalDepartment = new HospitalDepartmentValueModel
                          //{
                          //    Id = d.Id,
                          //    Name = d.Name,
                          //    DepartmentType = dt,
                          //    Hospital = new HospitalValueModel
                          //    {
                          //        Id = h.Id,
                          //        Name = h.Name,
                          //        Remark = h.Remark,
                          //    }
                          //}
                      };
            return new PagerResult<PrescriptionListApiModel>(query.Index, query.Size, sql);
        }
    }
}
