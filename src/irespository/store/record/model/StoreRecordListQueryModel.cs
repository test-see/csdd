using System;

namespace irespository.store.profile.model
{
    public class StoreRecordListQueryModel
    {
        public int? HospitalDepartmentId { get; set; }
        public int? HospitalGoodsId { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
