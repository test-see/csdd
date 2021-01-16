using System;

namespace irespository.client.goods.model
{
    public class ClientMappingGoodsListApiModel
    {
        public ClientGoodsValueModel Goods { get; set; }
        public int HospitalClientGoodsId { get; set; }
        public int ClientQty { get; set; }
        public int HospitalQty { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
    }
}
