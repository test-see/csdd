using foundation.config;
using irespository.checklist.goods.model;

namespace irespository.checklist.profile.model
{
    public class CheckListPreviewApiModel
    {
        public decimal Amount { get; set; }
        public PagerResult<CheckListGoodsPreviewListApiModel> CheckListGoods { get; set; }
    }
}
