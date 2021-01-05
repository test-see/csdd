using foundation.config;
using irespository.sys;
using irespository.sys.model;

namespace domain.sys
{
    public class EventlogContext
    {
        private readonly IEventlogRespository _sysEventlogRespository;
        public EventlogContext(IEventlogRespository sysEventlogRespository)
        {
            _sysEventlogRespository = sysEventlogRespository;
        }

        public PagerResult<EventlogListApiModel> GetPagerList(PagerQuery<EventlogListQueryModel> query)
        {
            return _sysEventlogRespository.GetPagerList(query);
        }
    }
}
