using foundation.config;
using foundation.ef5.poco;
using irespository.hospital.department.model;

namespace iservice.hospital
{
    public interface IHospitalDepartmentService
    {
        PagerResult<HospitalDepartmentListApiModel> GetPagerList(PagerQuery<HospitalDepartmentListQueryModel> query);
        HospitalDepartment Create(HospitalDepartmentCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, HospitalDepartmentUpdateApiModel updated, int userId);
    }
}
