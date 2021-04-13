using foundation.config;
using foundation.ef5;
using irespository.client.model;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.request.client
{
    public class ListingClientRequestHandler : IRequestHandler<PagerQuery<ListingClientRequest>, PagerResult<ListingClientResponse>>
    {
        private readonly DefaultDbContext _context;
        public ListingClientRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }

        public Task<PagerResult<ListingClientResponse>> Handle(IReceiveContext<PagerQuery<ListingClientRequest>> context, CancellationToken cancellationToken)
        {
            var sql = from r in _context.Client
                      join u in _context.User on r.CreateUserId equals u.Id
                      orderby r.Id descending
                      select new ListingClientResponse
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Name = r.Name,
                          CreateUserName = u.Username,
                      };
            if (!string.IsNullOrEmpty(context.Message.Query?.Name))
            {
                sql = sql.Where(x => x.Name.Contains(context.Message.Query.Name));
            }
            var pager = new PagerResult<ListingClientResponse>(context.Message.Index, context.Message.Size, sql);
            return Task.FromResult(pager);
        }
    }
}
