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
        public IEnumerable<Hospital> GetList()
        {
            return _context.Hospital.ToList();
        }
    }
}
