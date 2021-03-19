using foundation.config;
using foundation.ef5;
using irespository.sys;
using irespository.sys.model;
using System.Linq;

namespace respository.sys
{
    public class EventlogRespository : IEventlogRespository
    {
        private readonly DefaultDbContext _context;
        public EventlogRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public PagerResult<EventlogListApiModel> GetPagerList(PagerQuery<EventlogListQueryModel> query)
        {
            var sql = from r in _context.Eventlog
                      join u in _context.User on r.OptionUserId equals u.Id
                      //orderby r.Id descending
                      select new EventlogListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Content = r.Content,
                          OptionUsername = u.Username,
                          Title = r.Title,
                      };
            if (query.Query?.BeginDate != null)
            {
                sql=sql.Where(x => x.CreateTime >= query.Query.BeginDate.Value);
            }
            if (query.Query?.EndDate != null)
            {
                sql = sql.Where(x => x.CreateTime < query.Query.EndDate.Value.AddDays(1));
            }
            return new PagerResult<EventlogListApiModel>(query.Index, query.Size, sql);
        }
    }
}
