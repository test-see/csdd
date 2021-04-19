using irespository.hospital.department.model;
using irespository.hospital.goods.model;
using System;

namespace irespository.store.profile.model
{
    public class StoreListApiModel
    {
        public int Id { get; set; }
        public GetHospitalDepartmentResponse HospitalDepartment { get; set; }
        public GetHospitalGoodsResponse HospitalGoods { get; set; }
        public int Qty { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateUserName { get; set; }
    }
}
