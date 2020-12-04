using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using System.Collections.Generic;
using System.Linq;

namespace respository.hospital
{
    public class HospitalGoodsRespository : IHospitalGoodsRespository
    {
        private readonly DefaultDbContext _context;
        public HospitalGoodsRespository(DefaultDbContext context)
        {
            _context = context;
        }

        public IEnumerable<HospitalGoods> GetListByHospital(int hospitalId)
        {
            return _context.HospitalGoods.Where(x => x.HospitalId == hospitalId).ToList();
        }
    }
}
