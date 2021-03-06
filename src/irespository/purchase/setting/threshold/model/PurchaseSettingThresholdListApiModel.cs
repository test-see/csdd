using foundation.config;
using foundation.ef5.poco;
using irespository.hospital.client.model;
using irespository.hospital.goods.model;
using System;

namespace irespository.purchase.model
{
    public class PurchaseSettingThresholdListApiModel
    {
        public int Id { get; set; }
        public int HospitalDepartmentId { get; set; }
        public HospitalGoodsValueModel HospitalGoods { get; set; }
        public int UpQty { get; set; }
        public int DownQty { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
        public DataPurchaseThresholdType ThresholdType { get; set; }
    }
}
