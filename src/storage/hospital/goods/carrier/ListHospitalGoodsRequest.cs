namespace irespository.hospital.goods.model
{
    public class ListHospitalGoodsRequest
    {
        public int? HospitalId { get; set; }
        public string PinShou { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int? IsActive { get; set; }
    }
}
