using domain.hospital;
using foundation.config;
using foundation.ef5.poco;
using irespository.hospital.department.model;
using iservice.hospital;
using System.Collections.Generic;

namespace service.hospital
{
    public class HospitalDepartmentService : IHospitalDepartmentService
    {
        private readonly HospitalDepartmentContext _hospitalDepartmentContext;
        public HospitalDepartmentService(HospitalDepartmentContext HospitalDepartmentContext)
        {
            _hospitalDepartmentContext = HospitalDepartmentContext;
        }
        public PagerResult<HospitalDepartmentListApiModel> GetPagerList(PagerQuery<HospitalDepartmentListQueryModel> query)
        {
            return _hospitalDepartmentContext.GetPagerList(query);
        }
        public HospitalDepartment Create(HospitalDepartmentCreateApiModel created, int userId)
        {
            return _hospitalDepartmentContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _hospitalDepartmentContext.Delete(id);
        }

        public int Update(int id, HospitalDepartmentUpdateApiModel updated, int userId)
        {
            return _hospitalDepartmentContext.Update(id, updated);
        }
        public IEnumerable<DataDepartmentType> GetDepartmentTypeList()
        {
            return _hospitalDepartmentContext.GetDepartmentTypeList();
        }

        public IEnumerable<IdNameValueModel> GetParentList()
        {
            return _hospitalDepartmentContext.GetParentList();
        }
    }
}
