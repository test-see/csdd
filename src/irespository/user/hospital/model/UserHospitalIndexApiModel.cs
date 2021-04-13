using irespository.hospital.department.model;
using irespository.user.profile.model;
using System;

namespace irespository.user.hospital.model
{
    public class UserHospitalIndexApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GetHospitalDepartmentResponse HospitalDepartment { get; set; }
        public UserValueModel User { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
    }
}
