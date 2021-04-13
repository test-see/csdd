using foundation.config;
using foundation.ef5.poco;
using irespository.data;
using irespository.hospital;
using irespository.hospital.department.model;
using System.Collections.Generic;
using System.Linq;

namespace domain.hospital
{
    public class HospitalDepartmentContext
    {
        private readonly IHospitalDepartmentRespository _hospitalDepartmentRespository;
        private readonly IDepartmentTypeRespository _departmentTypeRespository;
        public HospitalDepartmentContext(IHospitalDepartmentRespository hospitalDepartmentRespository,
            IDepartmentTypeRespository departmentTypeRespository)
        {
            _hospitalDepartmentRespository = hospitalDepartmentRespository;
            _departmentTypeRespository = departmentTypeRespository;
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
        public int Update(int id, HospitalDepartmentUpdateApiModel updated)
        {
            return _hospitalDepartmentRespository.Update(id, updated);
        }

        public IEnumerable<DataDepartmentType> GetDepartmentTypeList()
        {
            return _departmentTypeRespository.GetList();
        }
        public IEnumerable<IdNameValueModel> GetParentList()
        {
            return _hospitalDepartmentRespository.GetParentList();
        }
        public GetHospitalDepartmentResponse GetValue(int id)
        {
            return _hospitalDepartmentRespository.GetValue(new int[] { id }).FirstOrDefault();
        }
    }
}
