using foundation.config;
using foundation.ef5;
using foundation.mediator;
using irespository.hospital.client.model;
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
    public class ListHospitalClientByPageRequestHandler : IRequestHandler<PagerQuery<ListHospitalClientRequest>, PagerResult<ListHospitalClientResponse>>
    {
        private readonly DefaultDbContext _context;
        private readonly IMediator _mediator;
        public ListHospitalClientByPageRequestHandler(DefaultDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<PagerResult<ListHospitalClientResponse>> Handle(IReceiveContext<PagerQuery<ListHospitalClientRequest>> context, CancellationToken cancellationToken)
        {
            var query = context.Message;
            var sql = from r in _context.HospitalClient
                      join u in _context.User on r.CreateUserId equals u.Id
                      orderby r.Id descending
                      select new ListHospitalClientResponse
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Name = r.Name,
                          Hospital = new GetHospitalResponse
                          {
                              Id = r.HospitalId,
                          },
                          CreateUserName = u.Username,
                      };
            if (query.Query?.HospitalId != null)
            {
                sql = sql.Where(x => x.Hospital.Id == query.Query.HospitalId.Value);
            }
            if (!string.IsNullOrEmpty(query.Query?.Name))
            {
                sql = sql.Where(x => x.Name.Contains(query.Query.Name));
            }
            var data = new PagerResult<ListHospitalClientResponse>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                var hospitals = await _mediator.ListByIdsAsync<GetHospitalRequest, GetHospitalResponse>(data.Select(x => x.Hospital.Id));

                foreach (var m in data.Result)
                {
                    m.Hospital = hospitals.FirstOrDefault(x => x.Id == m.Hospital.Id);
                }
            }
            return data;
        }
    }
}
