namespace irespository.hospital.goods.model
{
    public class HospitalGoodsUpdateApiModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Spec { get; set; }
        public string Unit { get; set; }
        public string Producer { get; set; }
        public int IsActive { get; set; }
        public string PinShou { get; set; }
        public decimal Price { get; set; }
        public string Barcode { get; set; }
    }
}
