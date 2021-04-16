using foundation.ef5;
using foundation.mediator;
using irespository.hospital.goods.model;
using irespository.hospital.profile.model;
using irespository.sys.model;
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
    public class GetHospitalGoodsRequestHandler : IRequestHandler<GetHospitalGoodsRequest, ListResponse<GetHospitalGoodsResponse>>
    {
        private readonly DefaultDbContext _context;
        private readonly IMediator _mediator;
        public GetHospitalGoodsRequestHandler(DefaultDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<ListResponse<GetHospitalGoodsResponse>> Handle(IReceiveContext<GetHospitalGoodsRequest> context, CancellationToken cancellationToken)
        {
            var payload = context.Message;
            var sql = from r in _context.HospitalGoods
                      join u in _context.User on r.CreateUserId equals u.Id
                      where payload.Ids.Contains(r.Id)
                      select new GetHospitalGoodsResponse
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Code = r.Code,
                          Barcode = r.Barcode,
                          Name = r.Name,
                          Hospital = new GetHospitalResponse
                          {
                              Id = r.HospitalId,
                          },
                          Producer = r.Producer,
                          Spec = r.Spec,
                          Unit = r.Unit,
                          CreateUserName = u.Username,
                          IsActive = r.IsActive,
                          PinShou = r.PinShou,
                          Price = r.Price,
                      };
            var profiles = await sql.ToListAsync();

            var hospitals = await _mediator.RequestListByIdsAsync<GetHospitalRequest, GetHospitalResponse>(profiles.Select(x => x.Hospital.Id).ToArray());
            var logs = await _mediator.RequestStorageAsync<ListEventlogByGoodsIdRequest, ListEventlogByGoodsIdResponse>(new ListEventlogByGoodsIdRequest { GoodsIds = profiles.Select(x => x.Id).ToArray() });

            foreach (var profile in profiles)
            {
                profile.Hospital = hospitals.FirstOrDefault(x=>x.Id== profile.Hospital.Id);
                profile.Logs = logs.Where(x => x.GoodsId == profile.Id).Select(x=>x.Log).ToList();
            }

            return new ListResponse<GetHospitalGoodsResponse>(profiles.ToArray());
        }
    }
}
