﻿using Mediator.Net.Contracts;

namespace irespository.hospital.goods.model
{
    public class CreateHospitalGoodsRequest : IRequest
    {
        public int HospitalId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string PinShou { get; set; }
        public string Spec { get; set; }
        public string Unit { get; set; }
        public string Producer { get; set; }
        public decimal Price { get; set; }
        public string Barcode { get; set; }
        public int UserId { get; set; }
    }
}
