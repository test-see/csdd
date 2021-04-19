using foundation.config;
using foundation.ef5;
using foundation.mediator;
using irespository.client.maping.model;
using irespository.client.maping.profile.model;
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
    public class ListClient2HospitalClientRequestHandler : IRequestHandler<PagerQuery<ListClient2HospitalClientRequest>, PagerResult<ListClient2HospitalClientResponse>>
    {
        private readonly DefaultDbContext _context;
        private readonly IMediator _mediator;
        public ListClient2HospitalClientRequestHandler(DefaultDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<PagerResult<ListClient2HospitalClientResponse>> Handle(IReceiveContext<PagerQuery<ListClient2HospitalClientRequest>> context, CancellationToken cancellationToken)
        {
            var query = context.Message;
            var sql = from p in _context.Client2HospitalClient
                      join u in _context.User on p.CreateUserId equals u.Id
                      join c in _context.HospitalClient on p.HospitalClientId equals c.Id
                      join h in _context.Hospital on c.HospitalId equals h.Id
                      join ct in _context.Client on p.ClientId equals ct.Id
                      orderby p.Id descending
                      select new ListClient2HospitalClientResponse
                      {
                          Client = new GetClientResponse
                          {
                              Id = ct.Id,
                              Name = ct.Name,
                          },
                          ClientMappingId = p.Id,
                          HospitalClient = new GetHospitalClientResponse
                          {
                              Id = c.Id,
                              Hospital = new GetHospitalResponse { Id = h.Id, }
                          },
                          CreateTime = p.CreateTime,
                          CreateUserName = u.Username,
                      };
            if (query.Query?.HospitalId != null)
            {
                sql = sql.Where(x => x.HospitalClient.Hospital.Id == query.Query.HospitalId.Value);
            }
            if (query.Query?.ClientId != null)
            {
                sql = sql.Where(x => x.Client.Id == query.Query.ClientId.Value);
            }
            if (!string.IsNullOrEmpty(query.Query?.Name))
            {
                sql = sql.Where(x => x.Client.Name.Contains(query.Query.Name));
            }
            var data = new PagerResult<ListClient2HospitalClientResponse>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                var hospitalclients = await _mediator.ListByIdsAsync<GetHospitalClientRequest, GetHospitalClientResponse>(data.Select(x => x.HospitalClient.Id));

                foreach (var m in data.Result)
                {
                    m.HospitalClient = hospitalclients.FirstOrDefault(x => x.Id == m.HospitalClient.Id);
                }
            }
            return data;
        }
    }
}
