using foundation.config;
using irespository.sys;
using irespository.sys.model;

namespace domain.sys
{
    public class EventlogContext
    {
        private readonly ISysEventlogRespository _sysEventlogRespository;
        public EventlogContext(ISysEventlogRespository sysEventlogRespository)
        {
            _sysEventlogRespository = sysEventlogRespository;
        }

        public PagerResult<EventlogListApiModel> GetPagerList(PagerQuery<EventlogListQueryModel> query)
        {
            return _sysEventlogRespository.GetPagerList(query);
        }
    }
}
