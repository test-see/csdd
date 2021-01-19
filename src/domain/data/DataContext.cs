using foundation.ef5.poco;
using irespository.data;
using System.Collections.Generic;

namespace domain.data
{
    public class DataContext
    {
        private readonly IAuthorizeRoleRespository _authorizeRoleRespository;
        public DataContext(IAuthorizeRoleRespository authorizeRoleRespository)
        {
            _authorizeRoleRespository = authorizeRoleRespository;
        }
        public IEnumerable<DataAuthorizeRole> GetDataAuthorizeList()
        {
            return _authorizeRoleRespository.GetList();
        }
    }
}
