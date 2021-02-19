using irespository.hospital.goods.model;
using System;

namespace irespository.purchase.model
{
    public class PurchaseGoodsBillnoIndexApiModel
    {
        public int Id { get; set; }
        public PurchaseIndexApiModel Purchase { get; set; }
        public HospitalGoodsValueModel HospitalGoods { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
        public string Billno { get; set; }
        public int Qty { get; set; }
        public DateTime Enddate { get; set; }
    }
}
