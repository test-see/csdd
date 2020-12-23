using foundation.ef5.poco;
using irespository.user;
using System.Collections.Generic;

namespace domain.sys
{
    public class PrivilegeContext
    {
        private readonly IDataMenuRespository _dataMenuRespository;
        public PrivilegeContext(IDataMenuRespository dataMenuRespository)
        {
            _dataMenuRespository = dataMenuRespository;
        }

        public IEnumerable<DataMenu> GetList()
        {
            return _dataMenuRespository.GetList();
        }
    }
}
