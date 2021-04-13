using irespository.hospital.department.model;
using System;

namespace irespository.purchase.model
{
    public class PurchaseIndexApiModel
    {
        public int Id { get; set; }
        public GetHospitalDepartmentResponse HospitalDepartment { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
        public int CreateUserId { get; set; }
        public int Status { get; set; }
        public int? PurchaseSettingId { get; set; }
    }
}
