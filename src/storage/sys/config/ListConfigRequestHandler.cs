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
    public class ListConfigRequestHandler : IRequestHandler<PagerQuery<ListConfigRequest>, PagerResult<ListConfigResponse>>
    {
        private readonly DefaultDbContext _context;
        public ListConfigRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }

        public Task<PagerResult<ListConfigResponse>> Handle(IReceiveContext<PagerQuery<ListConfigRequest>> context, CancellationToken cancellationToken)
        {
            var payload = context.Message;
            var sql = from r in _context.SysConfig
                                                     join u in _context.User on r.CreateUserId equals u.Id
                                                     select new ListConfigResponse
                                                     {
                                                         CreateTime = r.CreateTime,
                                                         Id = r.Id,
                                                         Value = r.Value,
                                                         Key = r.Key,
                                                         Remark = r.Remark,
                                                         CreateUserName = u.Username,
                                                     };
            var pager = new PagerResult<ListConfigResponse>(payload.Index, payload.Size, sql);
            return Task.FromResult(pager);
        }
    }
}
