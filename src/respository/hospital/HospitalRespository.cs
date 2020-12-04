using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using System.Collections.Generic;
using System.Linq;

namespace respository.hospital
{
    public class HospitalRespository: IHospitalRespository
    {
        private readonly DefaultDbContext _context;
        public HospitalRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Hospital> GetListByProvince(int provinceId)
        {
            return _context.Hospital.Where(x => x.ProvinceId == provinceId).ToList();
        }
    }
}
