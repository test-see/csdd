namespace irespository.purchase.model
{
    public class PurchaseSettingThresholdCreateApiModel
    {
        public int HospitalDepartmentId { get; set; }
        public int HospitalGoodsId { get; set; }
        public int UpQty { get; set; }
        public int DownQty { get; set; }
    }
}
