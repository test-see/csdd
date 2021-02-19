using System;

namespace irespository.purchase.model
{
    public class PurchaseGoodsBillnoUpdateApiModel
    {
        public int Qty { get; set; }
        public string Billno { get; set; }
        public DateTime Enddate { get; set; }
    }
}
