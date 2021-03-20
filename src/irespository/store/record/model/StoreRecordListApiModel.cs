using foundation.config;
using foundation.ef5.poco;
using irespository.hospital.department.model;
using irespository.hospital.goods.model;
using System;

namespace irespository.store.profile.model
{
    public class StoreRecordListApiModel//: OrderBy<int>
    {
        public int Id { get; set; }
        public HospitalDepartmentValueModel HospitalDepartment { get; set; }
        public HospitalGoodsValueModel HospitalGoods { get; set; }
        public int BeforeQty { get; set; }
        public DataStoreChangeType ChangeType { get; set; }
        public int ChangeQty { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
    }
}
