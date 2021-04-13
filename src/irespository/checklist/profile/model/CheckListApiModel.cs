using irespository.hospital.department.model;
using System;

namespace irespository.checklist.model
{
    public class CheckListApiModel
    {
        public int Id { get; set; }
        public GetHospitalDepartmentResponse HospitalDepartment { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
        public int Status { get; set; }
    }
}
