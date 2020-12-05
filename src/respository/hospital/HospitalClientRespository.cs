using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using System.Collections.Generic;
using System.Linq;

namespace respository.hospital
{
    public class HospitalClientRespository : IHospitalClientRespository
    {
        private readonly DefaultDbContext _context;
        public HospitalClientRespository(DefaultDbContext context)
        {
            _context = context;
        }

        public IEnumerable<HospitalClient> GetListByHospital(int hospitalId, string name)
        {
            return _context.HospitalClient.Where(x => x.HospitalId == hospitalId&& x.Name.Contains(name)).ToList();
        }
    }
}
