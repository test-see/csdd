﻿using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.department.model;
using irespository.hospital.profile.model;
using irespository.purchase;
using irespository.purchase.model;
using irespository.purchase.profile.enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace respository.purchase
{
    public class PurchaseRespository : IPurchaseRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalDepartmentRespository _hospitalDepartmentRespository;
        public PurchaseRespository(DefaultDbContext context,
            IHospitalDepartmentRespository hospitalDepartmentRespository)
        {
            _context = context;
            _hospitalDepartmentRespository = hospitalDepartmentRespository;
        }
        public PagerResult<PurchaseListApiModel> GetPagerList(PagerQuery<PurchaseListQueryModel> query)
        {
            var sql = from r in _context.Purchase
                      join u in _context.User on r.CreateUserId equals u.Id
                      join p in _context.HospitalDepartment on r.HospitalDepartmentId equals p.Id
                      orderby r.Id descending
                      select new PurchaseListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          CreateUserName = u.Username,
                          Name = r.Name,
                          Remark = r.Remark,
                          Status = r.Status,
                          HospitalDepartment = new HospitalDepartmentValueModel
                          {
                              Id = r.HospitalDepartmentId,
                              Hospital = new HospitalValueModel { Id = p.HospitalId }
                          }
                      };
            if (query.Query?.HospitalId != null)
            {
                sql = sql.Where(x => x.HospitalDepartment.Hospital.Id == query.Query.HospitalId.Value);
            }
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
            var data = new PagerResult<PurchaseListApiModel>(query.Index, query.Size, sql);
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

        public Purchase Create(PurchaseCreateApiModel created, int departmentId, int userId)
        {
            var setting = new Purchase
            {
                HospitalDepartmentId = departmentId,
                CreateUserId = userId,
                CreateTime = DateTime.Now,
                Name = created.Name,
                Remark = created.Remark,
                Status = (int)PurchaseStatus.Pendding,
                PurchaseSettingId = created.PurchaseSettingId,
            };

            _context.Purchase.Add(setting);
            _context.SaveChanges();

            return setting;
        }

        public int Delete(int id)
        {
            var thresholds = _context.PurchaseGoods.Where(x => x.PurchaseId == id); 
            if (thresholds.Any())
            {
                _context.PurchaseGoods.RemoveRange(thresholds);
                _context.SaveChanges();
            }

            var setting = _context.Purchase.Find(id);
            _context.Purchase.Remove(setting);
            _context.SaveChanges();
            return id;
        }

        public int Update(int id, PurchaseUpdateApiModel updated)
        {
            var setting = _context.Purchase.First(x => x.Id == id);
            setting.Name = updated.Name;
            setting.Remark = updated.Remark;

            _context.Purchase.Update(setting);
            _context.SaveChanges();
            return setting.Id;
        }

        public int UpdateStatus(int id, PurchaseStatus status)
        {
            var setting = _context.Purchase.First(x => x.Id == id);
            setting.Status = (int)status;

            _context.Purchase.Update(setting);
            _context.SaveChanges();
            return setting.Id;
        }

        public PurchaseIndexApiModel GetIndex(int id)
        {
            var sql = from r in _context.Purchase
                      join u in _context.User on r.CreateUserId equals u.Id
                      where r.Id == id
                      select new PurchaseIndexApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          CreateUserId = u.Id,
                          CreateUserName = u.Username,
                          Name = r.Name,
                          Remark = r.Remark,
                          Status = r.Status,
                          PurchaseSettingId = r.PurchaseSettingId,
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
        public IList<PurchaseValueModel> GetValue(int[] ids)
        {
            if (ids.Length == 0) return new List<PurchaseValueModel>();
            var sql = from r in _context.Purchase
                      where ids.Contains(r.Id)
                      select new PurchaseValueModel
                      {
                          Id = r.Id,
                          Name = r.Name,
                          Status = r.Status,
                          HospitalDepartment = new HospitalDepartmentValueModel
                          {
                              Id = r.HospitalDepartmentId,
                          }
                      };
            var setting = sql.ToList();
            var departments = _hospitalDepartmentRespository.GetValue(setting.Select(x => x.HospitalDepartment.Id).ToArray());
            foreach (var m in setting)
            {
                m.HospitalDepartment = departments.FirstOrDefault(x => x.Id == m.HospitalDepartment.Id);
            }
            return setting;

        }
    }
}
