﻿using irespository.hospital.client.model;
using irespository.hospital.goods.model;
using System;

namespace irespository.purchase.model
{
    public class PurchaseGoodsBillnoListApiModel
    {
        public int Id { get; set; }
        public PurchaseValueModel Purchase { get; set; }
        public GetHospitalGoodsResponse HospitalGoods { get; set; }
        public GetHospitalClientResponse HospitalClient { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
        public string Billno { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public DateTime Enddate { get; set; }
        public int? Status { get; set; }
        public int PurchaseGoodsId { get; set; }
        
    }
}
