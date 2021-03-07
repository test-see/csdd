using irespository.hospital.goods.model;

namespace irespository.prescription.model
{
    public class PrescriptionGoodsListApiModel
    {
        public int Id { get; set; }
        public int PrescriptionId { get; set; }
        public HospitalGoodsValueModel HospitalGoods { get; set; }
        public int Qty { get; set; }
    }
}
