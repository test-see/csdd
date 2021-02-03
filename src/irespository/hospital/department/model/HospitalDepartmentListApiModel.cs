using foundation.config;
using foundation.ef5.poco;
using irespository.hospital.profile.model;
using System;

namespace irespository.hospital.department.model
{
    public class HospitalDepartmentListApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HospitalValueModel Hospital { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
        public DataDepartmentType DepartmentType { get; set; }
        public IdNameValueModel Parent { get; set; }
        public int IsCheck { get; set; }
    }
}
