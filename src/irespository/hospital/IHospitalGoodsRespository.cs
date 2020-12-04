using foundation.ef5.poco;
using System.Collections.Generic;

namespace irespository.hospital
{
    public interface IHospitalGoodsRespository
    {
        IEnumerable<HospitalGoods> GetListByHospital(int hospitalId);
    }
}
