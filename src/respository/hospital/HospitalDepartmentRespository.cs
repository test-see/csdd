using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using System.Collections.Generic;
using System.Linq;

namespace respository.hospital
{
    public class HospitalDepartmentRespository : IHospitalDepartmentRespository
    {
        private readonly DefaultDbContext _context;
        public HospitalDepartmentRespository(DefaultDbContext context)
        {
            _context = context;
        }

        public IEnumerable<HospitalDepartment> GetListByHospital(int hospitalId)
        {
            return _context.HospitalDepartment.Where(x => x.HospitalId == hospitalId).ToList();
        }
    }
}
