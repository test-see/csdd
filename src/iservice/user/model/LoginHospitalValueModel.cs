using irespository.hospital.department.model;
using irespository.user.profile.model;

namespace domain.user.valuemodel
{
    public class LoginHospitalValueModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HospitalDepartmentValueModel HospitalDepartment { get; set; }
        public UserValueModel User { get; set; }
    }
}
