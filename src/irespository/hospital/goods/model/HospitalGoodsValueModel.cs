using irespository.hospital.profile.model;

namespace irespository.hospital.goods.model
{
    public class HospitalGoodsValueModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Spec { get; set; }
        public string Unit { get; set; }
        public string Producer { get; set; }
        public string PinShou { get; set; }
        public string Barcode { get; set; }
        public decimal Price { get; set; }
        public HospitalValueModel Hospital { get; set; }
    }
}
