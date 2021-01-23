namespace irespository.purchase.model
{
    public class PurchaseSettingThresholdUpdateApiModel
    {
        public int Id { get; set; }
        public int UpQty { get; set; }
        public int DownQty { get; set; }
        public int ThresholdTypeId { get; set; }
    }
}
