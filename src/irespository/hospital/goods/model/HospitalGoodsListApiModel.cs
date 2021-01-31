using irespository.hospital.profile.model;
using System;

namespace irespository.hospital.model
{
    public class HospitalGoodsListApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HospitalValueModel Hospital { get; set; }
        public string Spec { get; set; }
        public string UnitPurchase { get; set; }
        public string Producer { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
        public int IsActive { get; set; }
        public string PinShou { get; set; }
        public decimal Price { get; set; }
        public string Barcode { get; set; }
    }
}
