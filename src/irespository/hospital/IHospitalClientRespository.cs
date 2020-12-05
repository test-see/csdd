using foundation.ef5.poco;
using System.Collections.Generic;

namespace irespository.hospital
{
    public interface IHospitalClientRespository
    {
        IEnumerable<HospitalClient> GetListByHospital(int hospitalId, string name);
    }
}
