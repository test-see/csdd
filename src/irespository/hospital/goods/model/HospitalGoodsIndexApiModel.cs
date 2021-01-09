using irespository.hospital.profile.model;
using irespository.sys.model;
using System;
using System.Collections.Generic;

namespace irespository.hospital.goods.model
{
    public class HospitalGoodsIndexApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HospitalValueModel Hospital { get; set; }
        public string Spec { get; set; }
        public string UnitPurchase { get; set; }
        public string Producer { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
        public int IsActive { get; set; }

        public IList<EventlogListApiModel> Logs { get; set; }
    }
}
