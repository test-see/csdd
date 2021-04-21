using foundation.config;
using foundation.ef5;
using foundation.mediator;
using irespository.hospital.model;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client.profile
{
    public class ListHospitalByPageRequestHandler : IRequestHandler<PagerQuery<ListHospitalRequest>, PagerResult<ListHospitalResponse>>
    {
        private readonly DefaultDbContext _context;
        public ListHospitalByPageRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public Task<PagerResult<ListHospitalResponse>> Handle(IReceiveContext<PagerQuery<ListHospitalRequest>> context, CancellationToken cancellationToken)
        {
            var query = context.Message;
            var sql = from r in _context.Hospital
                      join u in _context.User on r.CreateUserId equals u.Id
                      orderby r.Id descending
                      select new ListHospitalResponse
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Name = r.Name,
                          Remark = r.Remark,
                          CreateUserName = u.Username,
                          ConsumeDays = r.ConsumeDays,
                      };
            return Task.FromResult(new PagerResult<ListHospitalResponse>(query.Index, query.Size, sql));
        }
    }
}
