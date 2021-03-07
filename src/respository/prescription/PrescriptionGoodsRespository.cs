using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.goods.model;
using irespository.prescription;
using irespository.prescription.model;
using System.Collections.Generic;
using System.Linq;

namespace respository.prescription
{
    public class PrescriptionGoodsRespository : IPrescriptionGoodsRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalGoodsRespository _hospitalGoodsRespository;
        public PrescriptionGoodsRespository(DefaultDbContext context,
            IHospitalGoodsRespository hospitalGoodsRespository)
        {
            _context = context;
            _hospitalGoodsRespository = hospitalGoodsRespository;
        }

        public IList<PrescriptionGoodsListApiModel> GetList(int prescriptionId)
        {
            var sql = from p in _context.PrescriptionGoods
                      where p.PrescriptionId == prescriptionId
                      select new PrescriptionGoodsListApiModel
                      {
                          Id = p.Id,
                          PrescriptionId = p.PrescriptionId,
                          Qty = p.Qty,
                          HospitalGoods = new HospitalGoodsValueModel { Id = p.HospitalGoodsId },
                      };
            var data = sql.ToList();
            var goods = _hospitalGoodsRespository.GetValue(data.Select(x => x.HospitalGoods.Id).ToArray());
            foreach (var m in data)
            {
                m.HospitalGoods = goods.FirstOrDefault(x => x.Id == m.HospitalGoods.Id);
            }
            return data;
        }

    }
}
