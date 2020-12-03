using irespository.user;
using iservice.data;
using iservice.data.model;

namespace service.data
{
    public class DataTouristRegisterService: IDataTouristRegisterService
    {
        private readonly IDataIdentityCategoryrRespository _dataIdentityCategoryrRespository;
        public DataTouristRegisterService(IDataIdentityCategoryrRespository dataIdentityCategoryrRespository)
        {
            _dataIdentityCategoryrRespository = dataIdentityCategoryrRespository;
        }

        public DataTouristRegisterApiModel GetData()
        {
            var data = new DataTouristRegisterApiModel();
            data.IdentityCategories = _dataIdentityCategoryrRespository.GetList();
            return data;
        }
    }
}
