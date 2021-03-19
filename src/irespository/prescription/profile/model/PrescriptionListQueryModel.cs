using System;

namespace irespository.prescription.model
{
    public class PrescriptionListQueryModel
    {
        public int? HospitalDepartmentId { get; set; }
        public string Cardno { get; set; }
        public int? Status { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
