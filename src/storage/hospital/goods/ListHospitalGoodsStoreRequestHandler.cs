using foundation.config;
using foundation.ef5;
using foundation.mediator;
using irespository.hospital.goods.model;
using irespository.hospital.model;
using irespository.hospital.profile.model;
using Mediator.Net;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using nouns.client.profile;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.request.client
{
    public class ListHospitalGoodsStoreRequestHandler : IRequestHandler<PagerQuery<ListHospitalGoodsStoreRequest>, PagerResult<ListHospitalGoodsStoreResponse>>
    {
        private readonly DefaultDbContext _context;
        private readonly IMediator _mediator;
        public ListHospitalGoodsStoreRequestHandler(DefaultDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<PagerResult<ListHospitalGoodsStoreResponse>> Handle(IReceiveContext<PagerQuery<ListHospitalGoodsStoreRequest>> context, CancellationToken cancellationToken)
        {
            var query = context.Message;
            var sql = from r in _context.HospitalGoods
                      join u in _context.User on r.CreateUserId equals u.Id
                      join s in _context.Store on new { HospitalGoodsId = r.Id, HospitalDepartmentId = query.Query.HospitalDepartmentId } equals new { s.HospitalGoodsId, s.HospitalDepartmentId } into ss
                      from ssx in ss.DefaultIfEmpty()
                      select new ListHospitalGoodsStoreResponse
                      {
                          CreateTime = r.CreateTime,
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
                          CreateUserName = u.Username,
                          IsActive = r.IsActive,
                          PinShou = r.PinShou,
                          Price = r.Price,
                          Barcode = r.Barcode,
                          Qty = ssx != null ? ssx.Qty : 0,
                      };
            if (!string.IsNullOrEmpty(query.Query?.PinShou))
            {
                sql = sql.Where(x => x.PinShou.Contains(query.Query.PinShou));
            }
            if (!string.IsNullOrEmpty(query.Query?.Barcode))
            {
                sql = sql.Where(x => x.Barcode.Contains(query.Query.Barcode));
            }
            if (!string.IsNullOrEmpty(query.Query?.Name))
            {
                sql = sql.Where(x => x.Name.Contains(query.Query.Name));
            }
            if (!string.IsNullOrEmpty(query.Query?.Code))
            {
                sql = sql.Where(x => x.Code.Contains(query.Query.Code));
            }
            if (query.Query?.IsActive != null)
            {
                sql = sql.Where(x => x.IsActive == query.Query.IsActive);
            }
            var data = new PagerResult<ListHospitalGoodsStoreResponse>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                var hospitals = await _mediator.RequestListByIdsAsync<GetHospitalRequest, GetHospitalResponse>(data.Select(x => x.Hospital.Id));
                foreach (var m in data.Result)
                {
                    m.Hospital = hospitals.FirstOrDefault(x => x.Id == m.Hospital.Id);
                }
            }
            return data;
        }
    }
}
