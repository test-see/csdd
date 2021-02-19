﻿using irespository.hospital.goods.model;
using System;

namespace irespository.client.goods.model
{
    public class ClientMappingGoodsListApiModel
    {
        public int Id { get; set; }
        public HospitalGoodsValueModel HospitalGoods { get; set; }
        public int ClientQty { get; set; }
        public int HospitalQty { get; set; }
    }
}
