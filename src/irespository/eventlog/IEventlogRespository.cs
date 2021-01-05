using foundation.config;
using irespository.sys.model;

namespace irespository.sys
{
    public interface IEventlogRespository
    {
        PagerResult<EventlogListApiModel> GetPagerList(PagerQuery<EventlogListQueryModel> query);
    }
}
