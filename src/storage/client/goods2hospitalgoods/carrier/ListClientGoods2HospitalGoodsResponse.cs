using irespository.hospital.goods.model;

namespace storage.clientgoods2hospitalgoods.carrier
{
    public class ListClientGoods2HospitalGoodsResponse
    {
        public int Id { get; set; }
        public int ClientGoodsId { get; set; }
        public GetHospitalGoodsResponse HospitalGoods { get; set; }
        public int ClientQty { get; set; }
        public int HospitalQty { get; set; }
    }
}
