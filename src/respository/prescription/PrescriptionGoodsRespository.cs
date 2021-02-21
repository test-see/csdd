using foundation.ef5;
using foundation.ef5.poco;
using irespository.prescription;
using System.Collections.Generic;
using System.Linq;

namespace respository.prescription
{
    public class PrescriptionGoodsRespository : IPrescriptionGoodsRespository
    {
        private readonly DefaultDbContext _context;
        public PrescriptionGoodsRespository(DefaultDbContext context)
        {
            _context = context;
        }

        public IList<PrescriptionGoods> GetList(int prescriptionId)
        {
            return _context.PrescriptionGoods.Where(x => x.PrescriptionId == prescriptionId).ToList();
        }

    }
}
