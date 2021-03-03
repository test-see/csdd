using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.department.model;
using irespository.invoice;
using irespository.invoice.model;
using irespository.invoice.profile.enums;
using System;
using System.Linq;

namespace respository.invoice
{
    public class InvoiceRespository : IInvoiceRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalDepartmentRespository _hospitalDepartmentRespository;
        public InvoiceRespository(DefaultDbContext context,
            IHospitalDepartmentRespository hospitalDepartmentRespository)
        {
            _context = context;
            _hospitalDepartmentRespository = hospitalDepartmentRespository;
        }
        public PagerResult<InvoiceListApiModel> GetPagerList(PagerQuery<InvoiceListQueryModel> query)
        {
            var sql = from r in _context.Invoice
                      join u in _context.User on r.CreateUserId equals u.Id
                      join t in _context.DataInvoiceType on r.InvoiceTypeId equals t.Id
                      select new InvoiceListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          CreateUserName = u.Username,
                          Name = r.Name,
                          Remark = r.Remark,
                          EndDate = r.EndDate,
                          StartDate = r.StartDate,
                          InvoiceType = t,
                          HospitalDepartment = new HospitalDepartmentValueModel { Id = r.HospitalDepartmentId, }
                      };
            if (query.Query?.Type != null)
            {
                sql = sql.Where(x => x.InvoiceType.Id == (int)query.Query.Type.Value);
            }
            var data = new PagerResult<InvoiceListApiModel>(query.Index, query.Size, sql);
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

        public Invoice Create(InvoiceCreateApiModel created, int departmentId, int userId)
        {
            var setting = new Invoice
            {
                HospitalDepartmentId = departmentId,
                CreateUserId = userId,
                CreateTime = DateTime.Now,
                Name = created.Name,
                Remark = created.Remark,
                Status = 0,
                EndDate = created.EndDate,
                StartDate = created.StartDate,
                InvoiceTypeId = created.InvoiceTypeId,
            };

            _context.Invoice.Add(setting);
            _context.SaveChanges();

            return setting;
        }

        public int Delete(int id)
        {
            var changetypes = _context.InvoiceReport.Where(x => x.InvoiceId == id);
            _context.InvoiceReport.RemoveRange(changetypes);
            _context.SaveChanges();
            foreach (var item in changetypes)
            {
                var records = _context.InvoiceReportRecord.Where(x => x.InvoiceReportId == item.Id);
                _context.InvoiceReportRecord.RemoveRange(records);
                _context.SaveChanges();
            }

            var setting = _context.Invoice.Find(id);
            _context.Invoice.Remove(setting);
            _context.SaveChanges();
            return id;
        }

        public int Update(int id, InvoiceUpdateApiModel updated)
        {
            var setting = _context.Invoice.First(x => x.Id == id);
            setting.Name = updated.Name;
            setting.Remark = updated.Remark;

            _context.Invoice.Update(setting);
            _context.SaveChanges();
            return setting.Id;
        }

        public int UpdateStatus(int id, InvoiceStatus status)
        {
            var setting = _context.Invoice.First(x => x.Id == id);
            setting.Status = (int)status;

            _context.Invoice.Update(setting);
            _context.SaveChanges();
            return setting.Id;
        }

        public InvoiceIndexApiModel GetIndex(int id)
        {
            var sql = from r in _context.Invoice
                      join u in _context.User on r.CreateUserId equals u.Id
                      join t in _context.DataInvoiceType on r.InvoiceTypeId equals t.Id
                      select new InvoiceIndexApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          CreateUserName = u.Username,
                          Name = r.Name,
                          Remark = r.Remark,
                          EndDate = r.EndDate,
                          StartDate = r.StartDate,
                          InvoiceType = t,
                          HospitalDepartment = new HospitalDepartmentValueModel
                          {
                              Id = r.HospitalDepartmentId,
                          }
                      };
            var setting = sql.FirstOrDefault();
            if (setting != null)
            {
                setting.HospitalDepartment = _hospitalDepartmentRespository.GetValue(new int[] { setting.HospitalDepartment.Id }).FirstOrDefault();
            }
            return setting;
        }

    }
}
