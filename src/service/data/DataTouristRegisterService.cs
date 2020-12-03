using irespository.user;
using iservice.data;
using iservice.data.model;

namespace service.data
{
    public class DataTouristRegisterService: IDataTouristRegisterService
    {
        private readonly IDataIdentityCategoryrRespository _dataIdentityCategoryrRespository;
        private readonly IDataProvinceRespository _dataProvinceRespository;
        public DataTouristRegisterService(IDataIdentityCategoryrRespository dataIdentityCategoryrRespository,
            IDataProvinceRespository dataProvinceRespository)
        {
            _dataIdentityCategoryrRespository = dataIdentityCategoryrRespository;
            _dataProvinceRespository = dataProvinceRespository;
        }

        public DataTouristRegisterApiModel GetData()
        {
            var data = new DataTouristRegisterApiModel();
            data.IdentityCategories = _dataIdentityCategoryrRespository.GetList();
            data.Provinces = _dataProvinceRespository.GetList();
            return data;
        }
    }
}
