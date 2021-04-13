using foundation.config;
using foundation.ef5;
using foundation.mediator;
using irespository.client.model;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.request.client
{
    public class ListClientStorageRequestHandler : IRequestHandler<StorageRequest<PagerQuery<ListClientRequest>>, PagerResult<ListClientResponse>>
    {
        private readonly DefaultDbContext _context;
        public ListClientStorageRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }

        public Task<PagerResult<ListClientResponse>> Handle(IReceiveContext<StorageRequest<PagerQuery<ListClientRequest>>> context, CancellationToken cancellationToken)
        {
            var payload = context.Message.Payload;
            var sql = from r in _context.Client
                      join u in _context.User on r.CreateUserId equals u.Id
                      orderby r.Id descending
                      select new ListClientResponse
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Name = r.Name,
                          CreateUserName = u.Username,
                      };
            if (!string.IsNullOrEmpty(payload.Query?.Name))
            {
                sql = sql.Where(x => x.Name.Contains(payload.Query.Name));
            }
            var pager = new PagerResult<ListClientResponse>(payload.Index, payload.Size, sql);
            return Task.FromResult(pager);
        }
    }
}
