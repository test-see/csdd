using foundation.ef5.poco;
using System.Collections.Generic;

namespace irespository.user
{
    public interface IDataProvinceRespository
    {
        IEnumerable<DataProvince> GetList();
    }
}
