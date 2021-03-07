using System;

namespace irespository.purchase.model
{
    public class PurchaseGoodsBillnoListQueryModel
    {
        public int? HospitalDepartmentId { get; set; }
        public int? HospitalGoodsId { get; set; }
        public int? HospitalClientId { get; set; }
        public string Billno { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
