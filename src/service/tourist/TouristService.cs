using foundation.ef5.poco;
using irespository.client;
using irespository.hospital;
using iservice.tourist;
using System.Collections.Generic;

namespace service.tourist
{
    public class TouristService : ITouristService
    {
        private readonly IHospitalRespository  _hospitalRespository;
        private readonly IClientRespository _clientRespository;
        private readonly IHospitalDepartmentRespository _hospitalDepartmentRespository;
        public TouristService(IHospitalRespository hospitalRespository,
            IClientRespository clientRespository,
            IHospitalDepartmentRespository hospitalDepartmentRespository)
        {
            _hospitalRespository = hospitalRespository;
            _clientRespository = clientRespository;
            _hospitalDepartmentRespository = hospitalDepartmentRespository;
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
    }
}
