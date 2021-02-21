using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.department.model;
using irespository.prescription;
using irespository.prescription.model;
using irespository.prescription.profile.enums;
using System;
using System.Linq;

namespace respository.prescription
{
    public class PrescriptionRespository : IPrescriptionRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalDepartmentRespository _hospitalDepartmentRespository;
        public PrescriptionRespository(DefaultDbContext context,
            IHospitalDepartmentRespository hospitalDepartmentRespository)
        {
            _context = context;
            _hospitalDepartmentRespository = hospitalDepartmentRespository;
        }

        public Prescription Create(PrescriptionCreateApiModel created, int departmentId, int userId)
        {
            var prescription = new Prescription
            {
                HospitalDepartmentId = departmentId,
                CreateUserId = userId,
                CreateTime = DateTime.UtcNow,
                Cardno = created.Cardno,
                Status = 1
            };
            _context.Prescription.Add(prescription);
            _context.SaveChanges();

            if (created.HospitalGoods != null && created.HospitalGoods.Any())
            {
                _context.PrescriptionGoods.AddRange(created.HospitalGoods.Select(x => new PrescriptionGoods
                {
                    HospitalGoodsId = x.Key,
                    PrescriptionId = prescription.Id,
                    Qty = x.Value,
                }));
                _context.SaveChanges();
            }

            return prescription;
        }

        public PagerResult<PrescriptionListApiModel> GetPagerList(PagerQuery<PrescriptionListQueryModel> query)
        {
            var sql = from p in _context.Prescription
                      join u in _context.User on p.CreateUserId equals u.Id
                      select new PrescriptionListApiModel
                      {
                          Cardno = p.Cardno,
                          CreateTime = p.CreateTime,
                          CreateUserName = u.Username,
                          Id = p.Id,
                          Status = p.Status,
                          HospitalDepartment = new HospitalDepartmentValueModel { Id = p.HospitalDepartmentId },
                      };
            var data = new PagerResult<PrescriptionListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                foreach (var m in data.Result)
                {
                    m.HospitalDepartment = _hospitalDepartmentRespository.GetValue(m.HospitalDepartment.Id);
                }
            }
            return data;
        }

        public int UpdateStatus(int id, PrescriptionStatus status)
        {
            var setting = _context.Prescription.First(x => x.Id == id);
            setting.Status = (int)status;

            _context.Prescription.Update(setting);
            _context.SaveChanges();
            return setting.Id;
        }

        public Prescription Get(int id)
        {
            return _context.Prescription.FirstOrDefault(x => x.Id == id);
        }
    }
}
