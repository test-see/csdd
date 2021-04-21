using foundation.config;
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
    public class ListWhitePhoneRequestHandler : IRequestHandler<ListWhitePhoneRequest, ListResponse<ListWhitePhoneResponse>>
    {
        private readonly DefaultDbContext _context;
        public ListWhitePhoneRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }

        public async Task<ListResponse<ListWhitePhoneResponse>> Handle(IReceiveContext<ListWhitePhoneRequest> context, CancellationToken cancellationToken)
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
            if (!string.IsNullOrEmpty(query?.Phone))
            {
                sql = sql.Where(x => x.Phone.Contains(query.Phone));
            }
            return (await sql.ToListAsync()).ToResponse();
        }
    }
}
