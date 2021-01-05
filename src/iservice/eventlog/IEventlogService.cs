using foundation.config;
using irespository.sys.model;

namespace iservice.sys
{
    public interface IEventlogService
    {
        PagerResult<EventlogListApiModel> GetPagerList(PagerQuery<EventlogListQueryModel> query);
    }
}
