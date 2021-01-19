using domain.data;
using foundation.ef5.poco;
using iservice.data;
using System.Collections.Generic;

namespace service.data
{
    public class DataService: IDataService
    {
        private readonly DataContext _dataContext;
        public DataService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IEnumerable<DataAuthorizeRole> GetDataAuthorizeList()
        {
            return _dataContext.GetDataAuthorizeList();
        }
    }
}
