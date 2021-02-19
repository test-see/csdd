using irespository.client.goods.model;
using irespository.purchase.model;

namespace domain.purchase.valuemodel
{
    public class PurchaseGoodsMappingListApiModel
    {
        public PurchaseGoodsListApiModel PurchaseGoods { get; set; }
        public ClientMappingGoodsIndexApiModel ClientMappingGoods { get; set; }
    }
}
