using domain.sys;
using foundation.config;
using irespository.sys.model;
using iservice.sys;

namespace service.sys
{
    public class EventlogService: IEventlogService
    {
        private readonly domain.sys.EventlogService _eventlogContext;
        public EventlogService(domain.sys.EventlogService eventlogContext)
        {
            _eventlogContext = eventlogContext;
        }
        public PagerResult<ListEventlogResponse> GetPagerList(PagerQuery<ListEventlogListRequest> query)
        {
            return _eventlogContext.GetPagerList(query);
        }
    }
}
