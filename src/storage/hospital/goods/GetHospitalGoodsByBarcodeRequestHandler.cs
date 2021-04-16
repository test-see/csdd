using foundation.ef5;
using foundation.mediator;
using irespository.hospital.goods.model;
using irespository.hospital.profile.model;
using Mediator.Net;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using Microsoft.EntityFrameworkCore;
using nouns.client.profile;
using storage.hospitalgoods.carrier;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client.profile
{
    public class GetHospitalGoodsByBarcodeRequestHandler : IRequestHandler<GetHospitalGoodsByBarcodeRequest, ListResponse<GetHospitalGoodsResponse>>
    {
        private readonly DefaultDbContext _context;
        private readonly IMediator _mediator;
        public GetHospitalGoodsByBarcodeRequestHandler(DefaultDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<ListResponse<GetHospitalGoodsResponse>> Handle(IReceiveContext<GetHospitalGoodsByBarcodeRequest> context, CancellationToken cancellationToken)
        {
            var payload = context.Message.Payload;
            var sql = from r in _context.HospitalGoods
                      join u in _context.User on r.CreateUserId equals u.Id
                      where r.Barcode == payload.Barcode.Trim()
                      select new GetHospitalGoodsResponse
                      {
                          Id = r.Id,
                          Name = r.Name,
                          Code = r.Code,
                          Hospital = new GetHospitalResponse
                          {
                              Id = r.HospitalId,
                          },
                          Producer = r.Producer,
                          Spec = r.Spec,
                          Unit = r.Unit,
                          PinShou = r.PinShou,
                          Price = r.Price,
                          Barcode = r.Barcode,
                      };
            var profiles = await sql.ToListAsync();

            var hospitals = await _mediator.RequestListByIdsAsync<GetHospitalRequest, GetHospitalResponse>(profiles.Select(x => x.Hospital.Id).ToArray());

            foreach (var profile in profiles)
            {
                profile.Hospital = hospitals.FirstOrDefault(x=>x.Id== profile.Hospital.Id);
            }

            return new ListResponse<GetHospitalGoodsResponse>(profiles.ToArray());
        }
    }
}
