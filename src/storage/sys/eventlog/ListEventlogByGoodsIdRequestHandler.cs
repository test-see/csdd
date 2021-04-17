using foundation.ef5;
using foundation.mediator;
using irespository.sys.model;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.request.client
{
    public class ListEventlogByGoodsIdRequestHandler : IRequestHandler<ListEventlogByGoodsIdRequest, ListResponse<ListEventlogByGoodsIdResponse>>
    {
        private readonly DefaultDbContext _context;
        public ListEventlogByGoodsIdRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }

        public async Task<ListResponse<ListEventlogByGoodsIdResponse>> Handle(IReceiveContext<ListEventlogByGoodsIdRequest> context, CancellationToken cancellationToken)
        {
            var query = context.Message;
            var sql = from g in _context.EventlogHospitalGoods
                      join r in _context.Eventlog on g.EventlogId equals r.Id
                      join u in _context.User on r.OptionUserId equals u.Id
                      where query.GoodsIds.Contains(g.HospitalGoodsId)
                      select new ListEventlogByGoodsIdResponse
                      {
                          GoodsId = g.HospitalGoodsId,
                          Log = new ListEventlogResponse
                          {
                              CreateTime = r.CreateTime,
                              Id = r.Id,
                              Content = r.Content,
                              OptionUsername = u.Username,
                              Title = r.Title,
                          }
                      };
            return (await sql.ToListAsync()).ToResponse();
        }
    }
}
