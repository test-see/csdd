using nouns.client.profile;
using storage.clientgoods2hospitalgoods.carrier;
using System;
using System.Collections.Generic;

namespace irespository.client.goods.model
{
    public class GetClientGoodsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public GetClientResponse Client { get; set; }
        public string Spec { get; set; }
        public string Unit { get; set; }
        public string Producer { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
        public int IsActive { get; set; }
        public IList<ListClientGoods2HospitalGoodsResponse> Mappings { get; set; }
    }
}
