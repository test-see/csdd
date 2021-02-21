using irespository.hospital.goods.model;
using System;

namespace irespository.checklist.goods.model
{
    public class CheckListGoodsPreviewListApiModel
    {
        public int Id { get; set; }
        public HospitalGoodsValueModel HospitalGoods { get; set; }
        public int StoreQty { get; set; }
        public int CheckQty { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUsername { get; set; }
        public decimal Amount => (CheckQty - StoreQty) * HospitalGoods.Price;
    }
}
