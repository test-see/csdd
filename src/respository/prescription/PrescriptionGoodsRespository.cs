using foundation.ef5;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital;
using irespository.hospital.goods.model;
using irespository.prescription;
using irespository.prescription.model;
using Mediator.Net;
using storage.hospitalgoods.carrier;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace respository.prescription
{
    public class PrescriptionGoodsRespository : IPrescriptionGoodsRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IMediator _mediator;
        public PrescriptionGoodsRespository(DefaultDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<IList<PrescriptionGoodsListApiModel>> GetListAsync(int prescriptionId)
        {
            var sql = from p in _context.PrescriptionGoods
                      where p.PrescriptionId == prescriptionId
                      select new PrescriptionGoodsListApiModel
                      {
                          Id = p.Id,
                          PrescriptionId = p.PrescriptionId,
                          Qty = p.Qty,
                          HospitalGoods = new GetHospitalGoodsResponse { Id = p.HospitalGoodsId },
                      };
            var data = sql.ToList();
            var goods = await _mediator.ListByIdsAsync<GetHospitalGoodsRequest, GetHospitalGoodsResponse>(data.Select(x => x.HospitalGoods.Id).ToList());

            foreach (var m in data)
            {
                m.HospitalGoods = goods.FirstOrDefault(x => x.Id == m.HospitalGoods.Id);
            }
            return data;
        }

    }
}
