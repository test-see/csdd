namespace irespository.hospital.goods.model
{
    public class HospitalGoodsCreateApiModel
    {
        public int HospitalId { get; set; }
        public string Name { get; set; }
        public string PinShou { get; set; }
        public string Spec { get; set; }
        public string UnitPurchase { get; set; }
        public string Producer { get; set; }
        public decimal Price { get; set; }
    }
}
