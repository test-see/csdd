namespace irespository.purchase.model
{
    public class PurchaseGoodsCreateApiModel
    {
        public int PurchaseId { get; set; }
        public int HospitalGoodsId { get; set; }
        public int Qty { get; set; }
        public int HospitalClientId { get; set; }
    }
}
