using irespository.hospital.goods.model;
using System;

namespace irespository.client.goods.model
{
    public class ClientMappingGoodsListApiModel
    {
        public HospitalGoodsValueModel HospitalGoods { get; set; }
        public int ClientQty { get; set; }
        public int HospitalQty { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
    }
}
