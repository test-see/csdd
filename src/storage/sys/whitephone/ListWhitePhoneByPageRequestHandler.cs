using foundation.config;
using foundation.ef5;
using irespository.sys.model;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.request.client
{
    public class ListWhitePhoneByPageRequestHandler : IRequestHandler<PagerQuery<ListWhitePhoneRequest>, PagerResult<ListWhitePhoneResponse>>
    {
        private readonly DefaultDbContext _context;
        public ListWhitePhoneByPageRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }

        public Task<PagerResult<ListWhitePhoneResponse>> Handle(IReceiveContext<PagerQuery<ListWhitePhoneRequest>> context, CancellationToken cancellationToken)
        {
            var query = context.Message;
            var sql = from r in _context.SysWhitePhone
                      join u in _context.User on r.CreateUserId equals u.Id
                      select new ListWhitePhoneResponse
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Phone = r.Phone,
                          CreateUserName = u.Username,
                      };
            if (!string.IsNullOrEmpty(query.Query?.Phone))
            {
                sql = sql.Where(x => x.Phone.Contains(query.Query.Phone));
            }
            var pager = new PagerResult<ListWhitePhoneResponse>(query.Index, query.Size, sql);
            return Task.FromResult(pager);
        }
    }
}
