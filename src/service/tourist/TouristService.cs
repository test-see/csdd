using foundation.ef5.poco;
using irespository.client;
using irespository.hospital;
using irespository.tourist.factory;
using irespository.tourist.model;
using iservice.tourist;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace service.tourist
{
    public class TouristService : ITouristService
    {
        private readonly IHospitalRespository  _hospitalRespository;
        private readonly IClientRespository _clientRespository;
        private readonly IHospitalDepartmentRespository _hospitalDepartmentRespository;
        private readonly IHospitalGoodsRespository _hospitalGoodsRespository;
        private readonly ITouristRegisterFactory _touristRegisterFactory;
        public TouristService(IHospitalRespository hospitalRespository,
            IClientRespository clientRespository,
            IHospitalDepartmentRespository hospitalDepartmentRespository,
            IHospitalGoodsRespository hospitalGoodsRespository,
            ITouristRegisterFactory touristRegisterFactory)
        {
            _hospitalRespository = hospitalRespository;
            _clientRespository = clientRespository;
            _hospitalDepartmentRespository = hospitalDepartmentRespository;
            _hospitalGoodsRespository = hospitalGoodsRespository;
            _touristRegisterFactory = touristRegisterFactory;
        }
        public IEnumerable<Hospital> GetHospitals(int provinceId)
        {
            return _hospitalRespository.GetListByProvince(provinceId);
        }
        public IEnumerable<Client> GetClients(int provinceId)
        {
            return _clientRespository.GetListByProvince(provinceId);
        }
        public IEnumerable<HospitalDepartment> GetHospitalDepartments(int hospitalId)
        {
            return _hospitalDepartmentRespository.GetListByHospital(hospitalId);
        }
        public IEnumerable<HospitalGoods> GetHospitalGoods(int hospitalId, string name)
        {
            return _hospitalGoodsRespository.GetListByHospital(hospitalId, name);
        }
        public async Task<int> CreateTouristAsync(TouristRegisterApiModel tourist)
        {
            return await _touristRegisterFactory.CreateTouristAsync(tourist);
        }
    }
}
