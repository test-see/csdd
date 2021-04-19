using foundation.config;
using irespository.hospital.goods.model;
using System;

namespace irespository.checklist.model
{
    public class CheckListGoodsListApiModel
    {
        public int Id { get; set; }
        public GetHospitalGoodsResponse HospitalGoods { get; set; }
        public int StoreQty { get; set; }
        public int CheckQty { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUsername { get; set; }
        public int CheckListId { get; set; }
    }
}
