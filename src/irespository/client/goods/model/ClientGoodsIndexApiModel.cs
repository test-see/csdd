using irespository.client.profile.model;
using System;
using System.Collections.Generic;

namespace irespository.client.goods.model
{
    public class ClientGoodsIndexApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public ClientValueModel Client { get; set; }
        public string Spec { get; set; }
        public string Unit { get; set; }
        public string Producer { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
        public int IsActive { get; set; }
        public IList<ClientGoods2HospitalGoodsListApiModel> Mappings { get; set; }
    }
}
