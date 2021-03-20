using irespository.hospital.department.model;
using System;

namespace irespository.purchase.model
{
    public class PurchaseValueModel
    {
        public int Id { get; set; }
        public HospitalDepartmentValueModel HospitalDepartment { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
    }
}
