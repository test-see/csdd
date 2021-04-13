using foundation.ef5.poco;
using irespository.hospital.department.model;
using System;

namespace irespository.storeinout.model
{
    public class StoreInoutIndexApiModel
    {
        public int Id { get; set; }
        public GetHospitalDepartmentResponse HospitalDepartment { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
        public DataStoreChangeType ChangeType { get; set; }
        public int Status { get; set; }
    }
}
