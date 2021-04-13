using irespository.hospital.department.model;
using System;
using System.Collections.Generic;

namespace irespository.prescription.model
{
    public class PrescriptionIndexApiModel
    {
        public int Id { get; set; }
        public GetHospitalDepartmentResponse HospitalDepartment { get; set; }
        public string Cardno { get; set; }
        public int Status { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
        public IList<PrescriptionGoodsListApiModel> PrescriptionGoods { get; set; }
    }
}
