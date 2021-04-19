using foundation.config;
using irespository.hospital.client.model;
using irespository.hospital.goods.model;
using System;

namespace irespository.purchase.model
{
    public class PurchaseGoodsListApiModel
    {
        public int Id { get; set; }
        public PurchaseValueModel Purchase { get; set; }
        public GetHospitalGoodsResponse HospitalGoods { get; set; }
        public int Qty { get; set; }
        public DateTime CreateTime { get; set; }
        public GetHospitalClientResponse HospitalClient { get; set; }
        public int Status { get; set; }
    }
}
