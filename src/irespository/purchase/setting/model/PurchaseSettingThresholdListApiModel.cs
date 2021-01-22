using irespository.hospital.department.model;
using System;

namespace irespository.purchase.model
{
    public class PurchaseSettingThresholdListApiModel
    {
        public int Id { get; set; }
        public HospitalDepartmentValueModel HospitalDepartment { get; set; }
        public int UpQty { get; set; }
        public int DownQty { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
    }
}
