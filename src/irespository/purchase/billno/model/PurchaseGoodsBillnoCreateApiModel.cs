using System;

namespace irespository.purchase.model
{
    public class PurchaseGoodsBillnoCreateApiModel
    {
        public int PurchaseGoodsId { get; set; }
        public int Qty { get; set; }
        public string Billno { get; set; }
        public DateTime Enddate { get; set; }

    }
}
