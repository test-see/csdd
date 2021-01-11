using domain.hospital;
using foundation.config;
using foundation.ef5.poco;
using irespository.hospital.department.model;
using iservice.hospital;

namespace service.hospital
{
    public class HospitalDepartmentService : IHospitalDepartmentService
    {
        private readonly HospitalDepartmentContext _HospitalDepartmentContext;
        public HospitalDepartmentService(HospitalDepartmentContext HospitalDepartmentContext)
        {
            _HospitalDepartmentContext = HospitalDepartmentContext;
        }
        public PagerResult<HospitalDepartmentListApiModel> GetPagerList(PagerQuery<HospitalDepartmentListQueryModel> query)
        {
            return _HospitalDepartmentContext.GetPagerList(query);
        }
        public HospitalDepartment Create(HospitalDepartmentCreateApiModel created, int userId)
        {
            return _HospitalDepartmentContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _HospitalDepartmentContext.Delete(id);
        }

        public int Update(HospitalDepartmentUpdateApiModel updated, int userId)
        {
            return _HospitalDepartmentContext.Update(updated);
        }
    }
}
