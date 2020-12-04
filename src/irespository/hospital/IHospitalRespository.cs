using foundation.ef5.poco;
using System.Collections.Generic;

namespace irespository.hospital
{
    public interface IHospitalRespository
    {
        IEnumerable<Hospital> GetListByProvince(int provinceId);
    }
}
