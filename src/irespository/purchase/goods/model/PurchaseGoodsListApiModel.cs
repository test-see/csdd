using foundation.config;
using irespository.hospital.goods.model;
using System;

namespace irespository.purchase.model
{
    public class PurchaseGoodsListApiModel
    {
        public int Id { get; set; }
        public HospitalGoodsValueModel HospitalGoods { get; set; }
        public int Qty { get; set; }
        public DateTime CreateTime { get; set; }
        public IdNameValueModel HospitalClient { get; set; }
    }
}
