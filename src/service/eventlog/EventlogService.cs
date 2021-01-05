using domain.sys;
using foundation.config;
using irespository.sys.model;
using iservice.sys;

namespace service.sys
{
    public class EventlogService: IEventlogService
    {
        private readonly EventlogContext _eventlogContext;
        public EventlogService(EventlogContext eventlogContext)
        {
            _eventlogContext = eventlogContext;
        }
        public PagerResult<EventlogListApiModel> GetPagerList(PagerQuery<EventlogListQueryModel> query)
        {
            return _eventlogContext.GetPagerList(query);
        }
    }
}
