namespace domain.user.valuemodel
{
    public class LoginHospitalValueModel
    {
        public int Id { get; set; }
        public int AuthorizeRoleId { get; set; }
        public int HospitalId { get; set; }
        public int HospitalDepartmentId { get; set; }
    }
}
