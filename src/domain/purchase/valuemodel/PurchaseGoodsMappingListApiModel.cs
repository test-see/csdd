using irespository.client.goods.model;
using irespository.purchase.goods.model;
using irespository.purchase.model;

namespace domain.purchase.valuemodel
{
    public class PurchaseGoodsMappingListApiModel
    {
        public PurchaseGoodsListApiModel PurchaseGoods { get; set; }
        public MappingClientGoodsValueModel MappingClientGoods { get; set; }
        //public int BillQty { get; set; }
    }
}
