using foundation.config;
using irespository.hospital.goods.model;
using System;

namespace irespository.storeinout.model
{
    public class StoreInoutGoodsListApiModel
    {
        public int Id { get; set; }
        public int StoreInoutId { get; set; }
        public HospitalGoodsValueModel HospitalGoods { get; set; }
        public int Qty { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
