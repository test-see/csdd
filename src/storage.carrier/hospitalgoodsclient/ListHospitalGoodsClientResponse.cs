﻿using irespository.hospital.client.model;
using irespository.hospital.goods.model;
using System;

namespace irespository.hospital.model
{
    public class ListHospitalGoodsClientResponse
    {
        public int Id { get; set; }
        public GetHospitalClientResponse HospitalClient { get; set; }
        public GetHospitalGoodsResponse HospitalGoods { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
    }
}
