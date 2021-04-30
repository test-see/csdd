using foundation.config;
using foundation.ef5;
using foundation.mediator;
using irespository.hospital.client.model;
using irespository.hospital.goods.model;
using irespository.hospital.model;
using Mediator.Net;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using Microsoft.EntityFrameworkCore;
using nouns.client.profile;
using storage.hospitalgoods.carrier;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.request.client
{
    public class ListHospitalGoodsClientRequestHandler : IRequestHandler<ListHospitalGoodsClientRequest, ListResponse<ListHospitalGoodsClientResponse>>
    {
        private readonly DefaultDbContext _context;
        private readonly IMediator _mediator;
        public ListHospitalGoodsClientRequestHandler(DefaultDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ListResponse<ListHospitalGoodsClientResponse>> Handle(IReceiveContext<ListHospitalGoodsClientRequest> context, CancellationToken cancellationToken)
        {
            var query = context.Message;
            var sql = from r in _context.HospitalGoodsClient
                      join u in _context.User on r.CreateUserId equals u.Id
                      orderby r.Id descending
                      select new ListHospitalGoodsClientResponse
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          CreateUserName = u.Username,
                          HospitalClient = new GetHospitalClientResponse { Id = r.HospitalClientId },
                          HospitalGoods = new GetHospitalGoodsResponse { Id = r.HospitalGoodsId },
                      };
            if (query?.HospitalClientId != null)
            {
                sql = sql.Where(x => x.HospitalClient.Id == query.HospitalClientId.Value);
            }
            if (query?.HospitalGoodsId != null)
            {
                sql = sql.Where(x => x.HospitalGoods.Id == query.HospitalGoodsId.Value);
            }
            var data = await sql.ToListAsync();

            var clients = await _mediator.ListByIdsAsync<GetHospitalClientRequest, GetHospitalClientResponse>(data.Select(x => x.HospitalClient.Id).ToList());
            var goods = await _mediator.ListByIdsAsync<GetHospitalGoodsRequest, GetHospitalGoodsResponse>(data.Select(x => x.HospitalGoods.Id).ToList());

            foreach (var m in data)
            {
                m.HospitalClient = clients.FirstOrDefault(x => x.Id == m.HospitalClient.Id);
                m.HospitalGoods = goods.FirstOrDefault(x => x.Id == m.HospitalGoods.Id);
            }
            return data.ToResponse();
        }
    }
}
