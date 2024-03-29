﻿using irespository.hospital.profile.model;
using irespository.sys.model;
using System;
using System.Collections.Generic;

namespace irespository.hospital.goods.model
{
    public class GetHospitalGoodsResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public GetHospitalResponse Hospital { get; set; }
        public string Spec { get; set; }
        public string Unit { get; set; }
        public string Producer { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
        public int IsActive { get; set; }
        public string PinShou { get; set; }
        public decimal Price { get; set; }
        public string Barcode { get; set; }

        public IList<ListEventlogResponse> Logs { get; set; }
    }
}
