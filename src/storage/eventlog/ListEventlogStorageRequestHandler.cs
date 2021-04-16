using foundation.config;
using foundation.ef5;
using foundation.mediator;
using irespository.sys.model;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.request.client
{
    public class ListEventlogStorageRequestHandler : IRequestHandler<StorageRequest<PagerQuery<ListEventlogRequest>>, PagerResult<ListEventlogResponse>>
    {
        private readonly DefaultDbContext _context;
        public ListEventlogStorageRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }

        public Task<PagerResult<ListEventlogResponse>> Handle(IReceiveContext<StorageRequest<PagerQuery<ListEventlogRequest>>> context, CancellationToken cancellationToken)
        {
            var query = context.Message.Payload;
            var sql = from r in _context.Eventlog
                      join u in _context.User on r.OptionUserId equals u.Id
                      orderby r.Id descending
                      select new ListEventlogResponse
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Content = r.Content,
                          OptionUsername = u.Username,
                          Title = r.Title,
                      };
            if (query.Query?.BeginDate != null)
            {
                sql = sql.Where(x => x.CreateTime >= query.Query.BeginDate.Value);
            }
            if (query.Query?.EndDate != null)
            {
                sql = sql.Where(x => x.CreateTime < query.Query.EndDate.Value.AddDays(1));
            }
            var pager = new PagerResult<ListEventlogResponse>(query.Index, query.Size, sql);
            return Task.FromResult(pager);
        }
    }
}
