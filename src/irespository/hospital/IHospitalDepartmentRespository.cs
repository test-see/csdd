using foundation.ef5.poco;
using System.Collections.Generic;

namespace irespository.hospital
{
    public interface IHospitalDepartmentRespository
    {
        IEnumerable<HospitalDepartment> GetListByHospital(int hospitalId);
    }
}
