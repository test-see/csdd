using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital;
using irespository.hospital.department.model;
using irespository.prescription;
using irespository.prescription.model;
using irespository.prescription.profile.enums;
using Mediator.Net;
using storage.hospital.department.carrier;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace respository.prescription
{
    public class PrescriptionRespository : IPrescriptionRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IPrescriptionGoodsRespository _prescriptionGoodsRespository;
        private readonly IMediator _mediator;
        public PrescriptionRespository(DefaultDbContext context,
            IMediator mediator,
            IPrescriptionGoodsRespository prescriptionGoodsRespository)
        {
            _context = context;
            _mediator = mediator;
            _prescriptionGoodsRespository = prescriptionGoodsRespository;
        }

        public Prescription Create(PrescriptionCreateApiModel created, int departmentId, int userId)
        {
            var prescription = new Prescription
            {
                HospitalDepartmentId = departmentId,
                CreateUserId = userId,
                CreateTime = DateTime.UtcNow,
                Cardno = created.Cardno,
                Status = (int)PrescriptionStatus.Pendding,
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

        public async Task<PagerResult<PrescriptionListApiModel>> GetPagerListAsync(PagerQuery<PrescriptionListQueryModel> query, int hospitalId)
        {
            var sql = from p in _context.Prescription
                      join u in _context.User on p.CreateUserId equals u.Id
                      join d in _context.HospitalDepartment on p.HospitalDepartmentId equals d.Id
                      where d.HospitalId == hospitalId
                      orderby p.Id descending
                      select new PrescriptionListApiModel
                      {
                          Cardno = p.Cardno,
                          CreateTime = p.CreateTime,
                          CreateUserName = u.Username,
                          Id = p.Id,
                          Status = p.Status,
                          HospitalDepartment = new GetHospitalDepartmentResponse { Id = p.HospitalDepartmentId },
                      };
            if (query.Query?.HospitalDepartmentId != null)
            {
                sql = sql.Where(x => x.HospitalDepartment.Id == query.Query.HospitalDepartmentId.Value);
            }
            if (!string.IsNullOrEmpty(query.Query?.Cardno))
            {
                sql = sql.Where(x => x.Cardno.Contains(query.Query.Cardno));
            }
            if (query.Query?.Status != null)
            {
                sql = sql.Where(x => x.Status == query.Query.Status.Value);
            }
            if (query.Query?.BeginDate != null)
            {
                sql = sql.Where(x => x.CreateTime >= query.Query.BeginDate.Value);
            }
            if (query.Query?.EndDate != null)
            {
                sql = sql.Where(x => x.CreateTime < query.Query.EndDate.Value.AddDays(1));
            }
            var data = new PagerResult<PrescriptionListApiModel>(query.Index, query.Size, sql);
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

        public int UpdateStatus(int id, PrescriptionStatus status)
        {
            var setting = _context.Prescription.First(x => x.Id == id);
            setting.Status = (int)status;

            _context.Prescription.Update(setting);
            _context.SaveChanges();
            return setting.Id;
        }

        public async Task<PrescriptionIndexApiModel> GetIndexAsync(int id)
        {
            var sql = from p in _context.Prescription
                      join u in _context.User on p.CreateUserId equals u.Id
                      join d in _context.HospitalDepartment on p.HospitalDepartmentId equals d.Id
                      where p.Id == id
                      select new PrescriptionIndexApiModel
                      {
                          Cardno = p.Cardno,
                          CreateTime = p.CreateTime,
                          CreateUserName = u.Username,
                          Id = p.Id,
                          Status = p.Status,
                          HospitalDepartment = new GetHospitalDepartmentResponse { Id = p.HospitalDepartmentId },
                      };
            var data = sql.FirstOrDefault();
            if(data!=null)
            {
                data.PrescriptionGoods = await _prescriptionGoodsRespository.GetListAsync(id);
            }
            return data;
        }
    }
}
