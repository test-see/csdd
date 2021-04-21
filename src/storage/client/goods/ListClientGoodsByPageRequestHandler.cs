using foundation.config;
using foundation.ef5;
using foundation.mediator;
using irespository.client.goods.model;
using irespository.client.model;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using nouns.client.profile;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.request.client
{
    public class ListClientGoodsByPageRequestHandler : IRequestHandler<PagerQuery<ListClientGoodsRequest>, PagerResult<ListClientGoodsResponse>>
    {
        private readonly DefaultDbContext _context;
        public ListClientGoodsByPageRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }

        public Task<PagerResult<ListClientGoodsResponse>> Handle(IReceiveContext<PagerQuery<ListClientGoodsRequest>> context, CancellationToken cancellationToken)
        {
            var query = context.Message;
            var sql = from r in _context.ClientGoods
                      join u in _context.User on r.CreateUserId equals u.Id
                      join h in _context.Client on r.ClientId equals h.Id
                      orderby r.Id descending
                      select new ListClientGoodsResponse
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Name = r.Name,
                          Code = r.Code,
                          Client = new GetClientResponse
                          {
                              Id = h.Id,
                              Name = h.Name,
                          },
                          Producer = r.Producer,
                          Spec = r.Spec,
                          Unit = r.Unit,
                          CreateUserName = u.Username,
                          IsActive = r.IsActive,
                      };
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
            if (query.Query?.ClientId != null)
            {
                sql = sql.Where(x => x.Client.Id == query.Query.ClientId.Value);
            }
            var pager = new PagerResult<ListClientGoodsResponse>(query.Index, query.Size, sql);
            return Task.FromResult(pager);
        }
    }
}
