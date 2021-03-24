using irespository.hospital.client.model;
using irespository.hospital.goods.model;
using System;

namespace irespository.hospital.model
{
    public class HospitalGoodsClientListApiModel
    {
        public int Id { get; set; }
        public HospitalClientValueModel HospitalClient { get; set; }
        public HospitalGoodsValueModel HospitalGoods { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
    }
}
