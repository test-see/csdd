using foundation.ef5.poco;
using irespository.hospital;
using iservice.tourist;
using System.Collections.Generic;

namespace service.tourist
{
    public class TouristService : ITouristService
    {
        private readonly IHospitalRespository  _hospitalRespository;
        public TouristService(IHospitalRespository hospitalRespository)
        {
            _hospitalRespository = hospitalRespository;
        }
        public IEnumerable<Hospital> GetHospitals(int provinceId)
        {
            return _hospitalRespository.GetListByProvince(provinceId);
        }
    }
}
