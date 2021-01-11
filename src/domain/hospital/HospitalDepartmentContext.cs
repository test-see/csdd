using foundation.config;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.department.model;

namespace domain.hospital
{
    public class HospitalDepartmentContext
    {
        private readonly IHospitalDepartmentRespository _hospitalDepartmentRespository;
        public HospitalDepartmentContext(IHospitalDepartmentRespository hospitalDepartmentRespository)
        {
            _hospitalDepartmentRespository = hospitalDepartmentRespository;
        }

        public PagerResult<HospitalDepartmentListApiModel> GetPagerList(PagerQuery<HospitalDepartmentListQueryModel> query)
        {
            return _hospitalDepartmentRespository.GetPagerList(query);
        }
        public HospitalDepartment Create(HospitalDepartmentCreateApiModel created, int userId)
        {
            return _hospitalDepartmentRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _hospitalDepartmentRespository.Delete(id);
        }
        public int Update(HospitalDepartmentUpdateApiModel updated)
        {
            return _hospitalDepartmentRespository.Update(updated);
        }
    }
}
