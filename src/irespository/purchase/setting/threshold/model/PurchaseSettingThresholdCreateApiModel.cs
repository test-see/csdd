namespace irespository.purchase.model
{
    public class PurchaseSettingThresholdCreateApiModel
    {
        public int PurchaseSettingId { get; set; }
        public int HospitalGoodsId { get; set; }
        public int UpQty { get; set; }
        public int DownQty { get; set; }
        public int ThresholdTypeId { get; set; }
    }
}
